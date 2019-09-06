using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esp32_indoor_localization

{
    class DevicePosition
    {
        public int Id { get; set; }
        public string Mac { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public Int32 timestamp { get; set; }

        public DevicePosition() {
        }
        public DevicePosition(string mac, double x, double y, Int32 timestamp)
        {
            this.Mac = mac;
            this.X = x;
            this.Y = y;
            this.timestamp = timestamp;
        }

  
    }
}
