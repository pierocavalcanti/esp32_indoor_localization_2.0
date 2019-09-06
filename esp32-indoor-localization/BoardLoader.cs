using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace esp32_indoor_localization
{
    public sealed class BoardLoader
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private List<Board> boards;
        private static readonly object padlock = new object();
        private static BoardLoader instance = null;
        private int numberOfBoards;

        BoardLoader()
        {
            try
            {
                using (System.IO.StreamReader r = new StreamReader(@"config.json"))
                {
                    string json = r.ReadToEnd();
                    log.Info("json letto: " + json);
                    log.Info("------------------------------------");
                    Boards = JsonConvert.DeserializeObject<List<Board>>(json);
                    NumberOfBoards = Boards.Count();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public List<Board> Boards { get => boards; set => boards = value; }

        public int NumberOfBoards { get => numberOfBoards; set => numberOfBoards = value; }

        public static BoardLoader Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new BoardLoader();
                        }
                    }
                }
                return instance;
            }
        }

        public Board GetBoardById(string id)
        {
            foreach (var board in Boards)
            {
                if (id == board.id) return board;
            }

            return null;
        }

        public Board GetBoardByMac(string mac)
        {
            foreach (var board in Boards)
            {
                if (mac == board.mac) return board;
            }

            return null;
        }

        public class Board
        {
            public string id { get; set; }
            public string mac { get; set; }
            public Double x { get; set; }
            public Double y { get; set; }

        }
    }
}
