using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using LiteDB;
using System.Windows.Forms;
using System.Diagnostics;
using log4net;
using System.Reflection;

namespace esp32_indoor_localization

{
    class PacketHandler
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void ProcessPacket(Packet packet, HttpListenerContext context)
        {
            //Il contesto passato dall'esterno ci permette di rispondere alle richieste nel case switch
            HttpListenerResponse response = context.Response;
            byte[] buffer;
            Stream st;
            
            switch (packet.type)
            {
                case "id_req":
                    string id = BoardLoader.Instance.GetBoardByMac(packet.mac).id;
                    buffer = Encoding.UTF8.GetBytes(id);
                    response.ContentLength64 = buffer.Length;
                    st = response.OutputStream;
                    st.Write(buffer, 0, buffer.Length);
                    response.Close();

                    //log.Info("ID: " + id + " assegnato alla board con mac " + packet.mac);
                    break;

                case "time_req":
                    //Tempo in secondi da unix_epoch
                    string timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
                    buffer = Encoding.UTF8.GetBytes(timestamp);
                    response.ContentLength64 = buffer.Length;
                    st = response.OutputStream;
                    st.Write(buffer, 0, buffer.Length);
                    response.Close();

                    //log.Info("ricevuta richiesta per il timestamp " + timestamp);

                    break;

                case "data":
                    //Apro il db (o lo creo se non esiste)
                    using (var db = new LiteDatabase(@"MyData.db"))
                    {
                        
                        //Prendo la collezione (o la creo se non esiste)
                        var col = db.GetCollection<Packet>("packets");
                        //Inserisco il nuovo documento Packet (l'ID è auto-incrementato)
                        col.Insert(packet);
                        //log.Info("pacchetto ricevuto da "+ packet.boardId +": " + Environment.NewLine + packet.toString());
                    }
                    response.Close();

                    break;
            }

        }
    }

}
