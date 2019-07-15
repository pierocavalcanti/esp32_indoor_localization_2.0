//using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
        }


        public List<DevicePosition> GetPositions(Int32 timestamp_from)
        {
            
            List<DevicePosition> positions  = null;
            
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<Packet>("packets");

                //Find hidden corrispondences:

                

                    //estraggo gli hash dei pacchetti validi ovvero faccio il controllo che siano pacchetti del timeslot corretto
                    // che i pacchetti siano stati ricevuti da tutti e tre le schedine (una schedina potrebbe aver catturato lo stesso pacchetto più volte!!)


                    var validHashes = col.Find(packet => packet.timestamp >= timestamp_from && packet.timestamp <= timestamp_from + 60 && packet.mac != "hidden")
                                     .GroupBy(x => new { x.mac, x.hash, x.boardId }, (key, packet) => new { key = key, Count = packet.Count() })
                                     .GroupBy(x => new { x.key.hash, x.key.mac }, (key, packet) => new { key = key, Count = packet.Count() })
                                     .Where(grp => grp.Count == BoardLoader.Instance.NumberOfBoards)
                                     .Select(p => p.key.hash);


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


               

                //mappa che contiene per ogni MAC una lista di stringhe del tipo 'ID'_'RSSI'
                Dictionary<string, List<string>> deviceInfo = new Dictionary<string, List<string>>();


                foreach (var info in deviceInfoList)
                {
                    string value = info.key.boardId + "_" + info.AVGRSSI.ToString();

                    if (!deviceInfo.ContainsKey(info.key.mac))
                    {
                        List<string> id_rssi_list = new List<string> { value };

                        deviceInfo.Add(info.key.mac, id_rssi_list);
                    }
                    else
                    {
                        deviceInfo[info.key.mac].Add(value);
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



            return positions;
            
        }

        /*public void RevealHiddenPositions(Int32 timestamp_from)
        {
            List<Packet> revealedHiddenPacket = new List<Packet>();
            Int32 sequenceNumber_threshold = 100;
            int device_marker = 0;

            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<Packet>("packets");

                List<Packet> hiddenPacket = col.Find(packet => packet.timestamp > timestamp_from && packet.timestamp < timestamp_from + 60 && packet.mac == "hidden").ToList(); //cast funziona??
                foreach (Packet p1 in hiddenPacket)
                {
                    hiddenPacket.Remove(p1);
                    string revealedMac = "hidden_" + device_marker;
                    p1.mac = revealedMac;
                    revealedHiddenPacket.Add(p1);
                    foreach (Packet p2 in hiddenPacket)
                    {
                        if (p1.sequenceNumber - p2.sequenceNumber < sequenceNumber_threshold)
                        {
                            hiddenPacket.Remove(p2);
                            p2.mac = revealedMac;
                            revealedHiddenPacket.Add(p2);
                        }
                    }
                    device_marker++;
                }






            }
        }*/


        private List<DevicePosition> Trilateration(Dictionary<string, List<string>> deviceInfo)
        {
            

            //Trilateration
            Double x_min_max = Double.MinValue;
            Double y_min_max = Double.MinValue;
            Double x_max_min = Double.MaxValue;
            Double y_max_min = Double.MaxValue;
            List<DevicePosition> positions = new List<DevicePosition>();

            foreach (var device in deviceInfo)
            {
                //log.Info("Sto processando il MAC: " + device.Key);
                foreach (var info in device.Value)
                {

                    String id = info.Split('_')[0];
                    Debug.WriteLine(info.Split('_')[1].Replace(',', '.'));
                    Double rssi = Double.Parse(info.Split('_')[1].Replace(',', '.'));
                    Double rssi_to_meters = ConvertRSSI(rssi);
                    Console.WriteLine("metri: " + rssi_to_meters);



                    //log.Info("[ id: " + id + ", rssi: " + rssi + ", rssi in meters: " + rssi_to_meters + "]");


                    //Calcolo X_min_max
                    //trovo il massimo delle ascisse minime -> max{ X_min[i] = ascissa di B[i] - convertedRSSI }

                    Console.WriteLine(BoardLoader.Instance.GetBoardById(id).x);

                    var x_min = BoardLoader.Instance.GetBoardById(id).x - rssi_to_meters;

                    if (x_min > x_min_max)
                    {
                        x_min_max = x_min;
                    }

                    //Calcolo X_max_min
                    //trovo il minimo delle ascisse massime -> X_max[i] = min{ ascissa di B[i] + convertedRSSI }

                    var x_max = BoardLoader.Instance.GetBoardById(id).x + rssi_to_meters;
                    if (x_max < x_max_min)
                    {
                        x_max_min = x_max;
                    }

                    //Calcolo Y_min_max
                    //trovo il massimo delle ordinate minime -> max{ Y_min[i] = ordinata di B[i] - convertedRSSI }
                    Console.WriteLine("y_min_max: " + y_min_max);
                    var y_min = BoardLoader.Instance.GetBoardById(id).y - rssi_to_meters;
                    if (y_min > y_min_max)
                    {
                        y_min_max = y_min;
                    }

                    //Calcolo Y_max_min
                    //trovo il minimo delle ascisse massime -> X_max[i] = min{ ascissa di B[i] + convertedRSSI }
                    var y_max = BoardLoader.Instance.GetBoardById(id).y + rssi_to_meters;
                    if (y_max < y_max_min)
                    {
                        y_max_min = y_max;
                    }


                }


                Double x = (x_min_max + x_max_min) / 2;
                Double y = (y_min_max + y_max_min) / 2;
                //log.Info("MAC " + device.Key + " con coordinate (" + x + "," + y + ") aggiunto");
                Int32 now = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                positions.Add(new DevicePosition(device.Key, x, y, now));
            }

            return positions;
        }


    }





}

