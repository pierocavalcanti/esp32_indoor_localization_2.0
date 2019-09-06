using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using LiteDB;
using log4net;

namespace esp32_indoor_localization
{

    class PositionHandler
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public double ConvertRSSI(double RSSI)
        {
            return 0.070871 * Math.Exp(0.062578 * - RSSI);
            /*var coeff = 4;
            var n = (-60 -RSSI)/(10*coeff);
            return  Math.Pow(10, n);*/

        }
        
        public List<DevicePosition> GetPositions(Int32 timestamp_from, double threshold/*, bool estimatePositions*/)
        {
            List<DevicePosition> results = new List<DevicePosition>();

            //find delle posizioni rilevata tra timestamp_from e timestamp_from + 60
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<DevicePosition>("positions");
                var oldPositions = col.Find(packet => packet.timestamp >= timestamp_from && packet.timestamp <= timestamp_from + 60);

                foreach (var position in oldPositions)
                {
                    results.Add(position);
                }
            }
            return results;
        }

        public void EstimateNotHiddenPositions()
        {
            
            Int32 timestamp_from = (Int32)Form_Home.DateTimeToUnixTimestamp(DateTime.Now);
            List<DevicePosition> positions  = null;
            
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<Packet>("packets");

                //estraggo gli hash dei pacchetti validi ovvero faccio il controllo che siano pacchetti del timeslot corretto
                // che i pacchetti siano stati ricevuti da tutti e tre le schedine (una schedina potrebbe aver catturato lo stesso pacchetto più volte!!)

                var validHashes = col.Find(packet => packet.timestamp >= timestamp_from - 60 && packet.timestamp <= timestamp_from && packet.mac != "hidden")
                                     .GroupBy(x => new { x.mac, x.hash, x.boardId }, (key, packet) => new { key = key, Count = packet.Count() })
                                     .GroupBy(x => new { x.key.hash, x.key.mac }, (key, packet) => new { key = key, Count = packet.Count() })
                                     .Where(grp => grp.Count == BoardLoader.Instance.NumberOfBoards)
                                     .Select(p => p.key.hash);


                Debug.WriteLine("Dispositivi Triangolati (query_1):"+validHashes.Count());

                //Non faccio il filter per timestamp incluso nella generazione dell'hash
                //seleziono MAC,ID,AVGRSSI
                var deviceInfoList = col.FindAll()
                                        .Where(p => validHashes.Contains(p.hash))
                                        .GroupBy(x => new { x.mac, x.boardId },
                                                     (key, values) => new
                                                     {
                                                         key = key,
                                                         AVGRSSI = values.Average(x => x.rssi)                                                     

                                                     }).ToList();

        

                //Debug.WriteLine("MAC+ID+AVG_RSSI trovati (query_2):"+deviceInfoList.Count());

                //mappa che contiene per ogni MAC una lista di stringhe del tipo 'ID'_'RSSI'
                Dictionary<string, List<string>> deviceInfo = new Dictionary<string, List<string>>();


                foreach (var info in deviceInfoList)
                {
                    string key = info.key.mac;
                    string value = info.key.boardId + "_" + info.AVGRSSI.ToString();

                    if (!deviceInfo.ContainsKey(key))
                    {
                        List<string> id_rssi_list = new List<string> { value };

                        deviceInfo.Add(key, id_rssi_list);
                    }
                    else
                    {
                        deviceInfo[key].Add(value);
                    }
                }

                //MAC,X,Y
                positions = Trilateration(deviceInfo);
                var colPositions = db.GetCollection<DevicePosition>("positions");
                foreach(DevicePosition position in positions)
                {
                    colPositions.Insert(position);
                }
            }
        }
                
        public void  EstimateHiddenPositions()
        {

            double threshold = Form_Home.getThreshold();

            Debug.WriteLine(threshold);

            Int32 timestamp_from = (Int32)Form_Home.DateTimeToUnixTimestamp(DateTime.Now);
            List<DevicePosition> hiddenPositions = null;

            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<Packet>("packets");
                //prendo gli hash di quei pacchetti che:
                //- sono stati ricevuti nello slot temporale corretto
                //- sono stati ricevuti da tutte e tre le schedine
                //N.B rispetto alla funzione standard qui lavoriamo sul sequenceNO e non sul mac dato che è casuale
                var validHiddenHashes = col.Find(packet => packet.timestamp >= timestamp_from && packet.timestamp <= timestamp_from + 60 && packet.mac == "hidden")
                                     .GroupBy(x => new { x.sequenceNumber, x.hash, x.boardId }, (key, packet) => new { key = key, Count = packet.Count() })
                                     .GroupBy(x => new { x.key.hash, x.key.sequenceNumber }, (key, packet) => new { key = key, Count = packet.Count() })
                                     .Where(grp => grp.Count == BoardLoader.Instance.NumberOfBoards)
                                     .Select(p => p.key.hash);
           

                // nel groupBy la presenza del mac sarà ininfluente nel raggruppamento ma servirà successivamente per assegnare differenziarli dai pacchetti standard
                var hiddenDeviceList = col.FindAll()
                                      .Where(p => validHiddenHashes.Contains(p.hash))
                                      .GroupBy(x => new { x.mac, x.hash , x.boardId },
                                                   (key, values) => new
                                                   {
                                                       key = key,
                                                       AVGRSSI = values.Average(x => x.rssi),
                                                      
                                                   }).ToList();
             

                Dictionary<string, List<string>> hiddenDeviceInfo = new Dictionary<string, List<string>>();

                foreach (var device in hiddenDeviceList)
                {
                    string key = device.key.hash.Substring(28) + "_hidden";
                    string value = device.key.boardId + "_" + device.AVGRSSI.ToString();

                    if (!hiddenDeviceInfo.ContainsKey(key))
                    {
                        List<string> id_rssi_list = new List<string> { value };
                        hiddenDeviceInfo.Add(key, id_rssi_list);
                    }
                    else
                    {
                        hiddenDeviceInfo[device.key.hash].Add(value);
                    }
                }

                hiddenPositions = Trilateration(hiddenDeviceInfo);
                List<DevicePosition> hiddenDevicePositions =  null;

                foreach (var position1 in hiddenPositions)
                {
                    hiddenDevicePositions.Add(position1);
                    hiddenPositions.Remove(position1);

                    foreach(var position2 in hiddenPositions)
                    {
                        if(EuclideanDistance(position1.X,position1.Y,position2.X,position2.Y,threshold))
                        {
                            hiddenPositions.Remove(position2);
                        }
                    }
                }

                if(hiddenDevicePositions!=null && hiddenDevicePositions.Count()!=0)
                {
                    var positionCollection = db.GetCollection<DevicePosition>("positions");

                    foreach (var position in hiddenDevicePositions)
                    {
                        positionCollection.Insert(position);
                    }
                }
            }
        }
    
        private List<DevicePosition> Trilateration(Dictionary<string, List<string>> deviceInfo)
        {
           
            Double x_min_max = Double.MinValue;
            Double y_min_max = Double.MinValue;
            Double x_max_min = Double.MaxValue;
            Double y_max_min = Double.MaxValue;
            Double x;
            Double y;
            List<DevicePosition> positions = new List<DevicePosition>();

            if(BoardLoader.Instance.NumberOfBoards == 2)
            {
                //se ci sono due board allora si calcola uno pseudo centro di massa pesato con le distanze misurate dalle due board
                foreach (var device in deviceInfo)
                {
                    x_min_max = Double.MinValue;
                    y_min_max = Double.MinValue;
                    x_max_min = Double.MaxValue;
                    y_max_min = Double.MaxValue;

                    String deviceId = device.Key;
                    
                    Double rssi1 = Double.Parse(device.Value[0].Split('_')[1].Replace(',', '.'));
                    Double R1 = ConvertRSSI(rssi1);
                    Double rssi2 = Double.Parse(device.Value[1].Split('_')[1].Replace(',', '.'));
                    Double R2 = ConvertRSSI(rssi2);
                    x = (R2 / (R1 + R2)) * BoardLoader.Instance.GetBoardById(device.Value[0].Split('_')[0]).x + (R1 / (R1 + R2)) * BoardLoader.Instance.GetBoardById(device.Value[1].Split('_')[0]).x;
                    y = (R2 / (R1 + R2)) * BoardLoader.Instance.GetBoardById(device.Value[0].Split('_')[0]).y + (R1 / (R1 + R2)) * BoardLoader.Instance.GetBoardById(device.Value[1].Split('_')[0]).y;

                    Int32 timestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                    positions.Add(new DevicePosition(deviceId, x, y, timestamp));
                }
            }
            else
            {
                foreach (var device in deviceInfo)
                {
                    x_min_max = Double.MinValue;
                    y_min_max = Double.MinValue;
                    x_max_min = Double.MaxValue;
                    y_max_min = Double.MaxValue;

                    String deviceId = device.Key;


                    //log.Info("Sto processando il MAC: " + mac);
                    foreach (var info in device.Value)
                    {

                        String id = info.Split('_')[0];
                        log.Info("rssi_old: " + info.Split('_')[1]);
                        Double rssi = Double.Parse(info.Split('_')[1].Split(',')[0]);
                        Double rssi_to_meters = ConvertRSSI(rssi);


                        log.Info("[ id: " + id + ", MAC: " + deviceId + ", rssi: " + rssi + ", rssi in meters: " + rssi_to_meters + "]");


                        //Calcolo X_min_max
                        //trovo il massimo delle ascisse minime -> max{ X_min[i] = ascissa di B[i] - convertedRSSI }

                        double x_min = BoardLoader.Instance.GetBoardById(id).x - rssi_to_meters;

                        if (x_min > x_min_max)
                        {
                            x_min_max = x_min;
                        }

                        //Calcolo X_max_min
                        //trovo il minimo delle ascisse massime -> X_max[i] = min{ ascissa di B[i] + convertedRSSI }

                        double x_max = BoardLoader.Instance.GetBoardById(id).x + rssi_to_meters;
                        if (x_max < x_max_min)
                        {
                            x_max_min = x_max;
                        }

                        //Calcolo Y_min_max
                        //trovo il massimo delle ordinate minime -> max{ Y_min[i] = ordinata di B[i] - convertedRSSI }

                        double y_min = BoardLoader.Instance.GetBoardById(id).y - rssi_to_meters;
                        if (y_min > y_min_max)
                        {
                            y_min_max = y_min;
                        }

                        //Calcolo Y_max_min
                        //trovo il minimo delle ascisse massime -> X_max[i] = min{ ascissa di B[i] + convertedRSSI }
                        double y_max = BoardLoader.Instance.GetBoardById(id).y + rssi_to_meters;
                        if (y_max < y_max_min)
                        {
                            y_max_min = y_max;
                        }


                    }

                    
                    x = (x_min_max + x_max_min) / 2;
                    y = (y_min_max + y_max_min) / 2;
                    log.Info("x: " + x + ", y: " + y + ", xminmax:" + x_min_max + ", xmaxmin:" + x_max_min + ", yminmax:" + y_min_max + ", ymaxmin:" + y_max_min);

                    Int32 timestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                    positions.Add(new DevicePosition(deviceId, x, y, timestamp));
                    log.Info("MAC " + device.Key + " con coordinate (" + x + "," + y + timestamp + ") aggiunto");
                }
                log.Info("-----------------------------");
            }
        return positions;
        }

        private Boolean EuclideanDistance(double x1, double y1, double x2, double y2, double threshold)
        {
            return (Math.Sqrt(((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2))) <= threshold) ? true : false;
        }
    }
}

