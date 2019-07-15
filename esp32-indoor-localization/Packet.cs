using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esp32_indoor_localization

{
    class Packet
    {
        public int Id { get; set; }
        public string type { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string boardId { get; set; }
        public Int32 timestamp { get; set; }
        public Int32 rssi { get; set; }
        public string mac { get; set; }
        public string ssid { get; set; }
        public string hash { get; set; }
        public Int32 sequenceNumber { get; set; }

        public string toString()
        {
            return "pacchetto ricevuto: { mac: " + this.mac + 
                                        ", id: " + this.boardId + 
                                        ", timestamp: " +  this.timestamp + 
                                        ", rssi: " + this.rssi + 
                                        ", ssid: " + this.ssid +
                                        ", hash: " + this.hash;
        }
    }
}
