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

        
        public async Task<List<DevicePosition>[]> GetPositions(Int32 timestamp_from, double threshold)
        {
            //eseguo due task: uno per trovare le posizioni standard e l'altro per trovare i mac nascosti
            Task<List<DevicePosition>> mainTask = EstimateNotHiddenPositions(timestamp_from);
            Task<List<DevicePosition>> hiddenTask = EstimateHiddenPositions(timestamp_from, threshold);

            List<DevicePosition>[] results = await Task.WhenAll(mainTask, hiddenTask);

            return results;
        }


        private async Task<List<DevicePosition>> EstimateNotHiddenPositions(Int32 timestamp_from)
        {
            Debug.WriteLine("not hidden");
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
                                                        AVGRSSI = values.Average(x => x.rssi),
                                                        AVGTimestamp = values.Average(x => x.timestamp)

                                                    }).ToList();


               

                //mappa che contiene per ogni MAC una lista di stringhe del tipo 'ID'_'RSSI'
                Dictionary<string, List<string>> deviceInfo = new Dictionary<string, List<string>>();


                foreach (var info in deviceInfoList)
                {
                    string key = info.key.mac + "_" + info.AVGTimestamp.ToString();
                    string value = info.key.boardId + "_" + info.AVGRSSI.ToString();

                    if (!deviceInfo.ContainsKey(info.key.mac))
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
                       

            return positions;
            
        }

        
        private async Task<List<DevicePosition>>  EstimateHiddenPositions(Int32 timestamp_from, double threshold)
        {
            
            List<DevicePosition> hiddenPositions = null;

            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<Packet>("packets");
                                
                var validHiddenHashes = col.Find(packet => packet.timestamp >= timestamp_from && packet.timestamp <= timestamp_from + 60 && packet.mac == "hidden")
                                     .GroupBy(x => new { x.sequenceNumber, x.hash, x.boardId }, (key, packet) => new { key = key, Count = packet.Count() })
                                     .GroupBy(x => new { x.key.hash, x.key.sequenceNumber }, (key, packet) => new { key = key, Count = packet.Count() })
                                     .Where(grp => grp.Count == BoardLoader.Instance.NumberOfBoards)
                                     .Select(p => p.key.hash);

                var hiddenDeviceList = col.FindAll()
                                      .Where(p => validHiddenHashes.Contains(p.hash))
                                      .GroupBy(x => new { x.mac, x.hash , x.boardId },
                                                   (key, values) => new
                                                   {
                                                       key = key,
                                                       AVGRSSI = values.Average(x => x.rssi),
                                                       AVGTimestamp = values.Average(x => x.timestamp)

                                                   }).ToList();

                Dictionary<string, List<string>> hiddenDeviceInfo = new Dictionary<string, List<string>>();


                foreach (var device in hiddenDeviceList)
                {
                    string key = device.key.hash + "_" + device.AVGTimestamp.ToString();
                    string value = device.key.boardId + "_" + device.AVGRSSI.ToString();

                    if (!hiddenDeviceInfo.ContainsKey(device.key.hash))
                    {
                        List<string> id_rssi_list = new List<string> { value };

                        hiddenDeviceInfo.Add(device.key.hash, id_rssi_list);
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

                    foreach(var position in hiddenDevicePositions)
                    {
                        positionCollection.Insert(position);
                    }

                    return hiddenDevicePositions;
                }


            }
            
          
            return null;
        }

    
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
                String mac = device.Key.Split('_')[0];
                Int32 timestamp = Int32.Parse(device.Key.Split('_')[1]);

                //log.Info("Sto processando il MAC: " + mac);
                foreach (var info in device.Value)
                {

                    String id = info.Split('_')[0];
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
                //Int32 now = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                if (mac == "hidden") mac = timestamp.GetHashCode().ToString();
                positions.Add(new DevicePosition(mac, x, y, timestamp));
            }

            return positions;
        }

        private Boolean EuclideanDistance(double x1, double y1, double x2, double y2, double threshold)
        {
            return (Math.Sqrt(((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2))) <= threshold) ? true : false;

            
        }


    }





}

