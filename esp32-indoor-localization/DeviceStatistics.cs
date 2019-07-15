using LiteDB;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace esp32_indoor_localization
{
    class DeviceStatistics
    {

        private static readonly ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static List<DeviceCount> CountDevice(Int32 start, Int32 end)
        {

            List<DeviceCount> devices = null;
            using (var db = new LiteDatabase(@"MyData.db"))
            {

                var col = db.GetCollection<Packet>("packets");

                //selezioni i pacchetti appartenenti alla finestra temporale passata per parametro
                // raggruppo per mac, conto e proietto il timestamp minimo e massimo
                var countDeviceList = col.Find(packet => packet.mac != "hidden" && packet.timestamp >= start && packet.timestamp <= end)
                                         .GroupBy(x => new { x.mac }, (key, packet) => new { key = key, Count = packet.Count(), time_start = packet.Min(p => p.timestamp), time_end = packet.Max(p => p.timestamp) }).ToList();


                if (countDeviceList.Count != 0)
                {

                    devices = new List<DeviceCount>();

                    foreach (var device in countDeviceList)
                    {

                        devices.Add(new DeviceCount(device.key.mac, device.Count, device.time_start, device.time_end));
                    }

                    return devices;
                }
                else
                {
                    log.Info("Nessuna rilevazione in questa finestra temporale");
                }

            }

            return null;
        }




        public class DeviceCount
        {
            private string mac;
            private Int32 counter;
            private Int32 timestamp_start;
            private Int32 timestamp_end;

            public DeviceCount(string mac, int counter, int timestamp_start, int timestamp_end)
            {
                this.mac = mac;
                this.counter = counter;
                this.timestamp_start = timestamp_start;
                this.timestamp_end = timestamp_end;
            }



            public string Mac { get => mac; set => mac = value; }
            public int Counter { get => counter; set => counter = value; }
            public int Timestamp_start { get => timestamp_start; set => timestamp_start = value; }
            public int Timestamp_end { get => timestamp_end; set => timestamp_end = value; }

        }


    }
}
