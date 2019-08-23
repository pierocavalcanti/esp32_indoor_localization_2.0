using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using LiteDB;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using log4net;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;

//using NLog;


namespace esp32_indoor_localization
{

    public partial class Form_Home : Form
    {

        
        const bool connectionIsPossible = true; //FOR DEBUG, SET TRUE IF CONNECTION IS ON!
        private const string IP_ADDRESS_SERVER = "http://192.168.1.16:3000/";


        private static readonly ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        System.Windows.Forms.Timer t;
        List<BoardLoader.Board> boards;
        List<DevicePosition> devices;
        PositionHandler positionHandler;
        static DateTime startUpDateTime = DateTime.Now; //momento di apertura del programma per StatsChart
        private string std_password = "923d0f5c9146b3391c711c052af598c4b7a1d3daa6d4ec6d41ffcc22d9ddf32d";

        //VISUALIZED INTERVAL INDICA IL MINUTO CHE è VISUALIZZATO NELLA MAPPA
        MinuteInterval visualizedInterval = new MinuteInterval(DateTime.Now);

        //PER CONTEGGIO DISPOSITIVI OGNI MINUTO:
        List<int> counts_stats = new List<int>();

        class MinuteInterval
        {
            // Classe che descrive un minuto, in modo da poter usare la classe per gestire l'intervallo visualizzato nella mappa
            // con le proprietà from e to accedi ai margini dell'intervallo
            // con add e subtract aggiungi il quantitativo di secondi, traslando l'intero intervallo
            // è possibile ottenere i due valore from e to in formato UnixTimeStamp
            public DateTime from { get; set; }
            public DateTime to { get; set; }

            public double getFromUnixTimestamp() {
                return DateTimeToUnixTimestamp(from);
            }
            public double getToUnixTimestamp() {
                return DateTimeToUnixTimestamp(to);
            }

            public MinuteInterval(DateTime val)
            {
                // costruttore parte dal minuto iniziale
                to = val;
                from = val.AddSeconds(-60);
            }

            public void add(Int64 val) {
               // Debug.WriteLine("ADD -> "+val);
                from = from.AddSeconds(val);
                to = to.AddSeconds(val);
            }

            public void subtract(Int64 val)
            {
               // Debug.WriteLine("SUBTRACT -> " + val);
                val = -val;
                from = from.AddSeconds(val);
                to = to.AddSeconds(val);
            }

            public override bool Equals(object obj)
            {
                return obj is MinuteInterval interval &&
                       from == interval.from &&
                       to == interval.to;
            }
        }

        public Form_Home()
        {
            //Inizializza form
            InitializeComponent();
            //Crea un nuovo Thread separato per il server (while true loop che genera un Thread ad ogni richiesta)

            t = new System.Windows.Forms.Timer();


            //Leggo il file di config delle boards e accedo alla lista delle board. BoardLoader classe Singleton
            boards = BoardLoader.Instance.Boards;

            positionHandler = new PositionHandler();

            RequireCredential();
            
            //fixSize();

            //visualizedInterval = new MinuteInterval(DateTime.Now);

            counts_stats = new List<int>();

        }

        public void RequireCredential()
        {
            PasswordForm frm = new PasswordForm();
            string psw_inserita = frm.Show("Enter Password:", "Password"); //password ritornata dal form
            using (SHA256 sha256Hash = SHA256.Create())
            {
                if (!(VerifyHash(sha256Hash, psw_inserita,std_password)))
                {
                    this.Close();
                }
            }


        }

        public void fixSize()
        {
            //funzione per definire una dimensione fissa della finestra
            
            // Define the border style of the form to a dialog box.
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Set the MaximizeBox to false to remove the maximize box.
            this.MaximizeBox = false;

            // Set the MinimizeBox to false to remove the minimize box.
            this.MinimizeBox = false;

            // Set the start position of the form to the center of the screen.
            this.StartPosition = FormStartPosition.CenterScreen;

            // Display the form as a modal dialog box.
            this.ShowDialog();
        }

        public void AsyncServer()
        {
            //Server HTTP asincrono
            var listener = new HttpListener();
            listener.Prefixes.Add(IP_ADDRESS_SERVER);
            listener.Start();
            Debug.WriteLine(listener.Prefixes.First<String>());
            while (true)
            {
                try
                {
                    Debug.WriteLine("sono nel thread");
                    //Genera un thread ad ogni richiesta
                    var context = listener.GetContext();
                    Debug.WriteLine("sono nel thread 2" + context.Request.HttpMethod);
                    bool esiste = ThreadPool.QueueUserWorkItem(o => HandleRequest(context));
                    Debug.WriteLine("thread pool funziona?" + esiste);
                }
                catch (Exception e)
                {
                    //TODO Stampa su log4net
                    Debug.WriteLine(e.Message);
                }
            }
        }
        private void HandleRequest(object state)
        {
            try
            {
                var context = (HttpListenerContext)state;
                //Processa la risposta
                //Legge l'encoding e decodifica in maniera opportuna il body
                Stream body = context.Request.InputStream;
                Encoding encoding = context.Request.ContentEncoding;
                StreamReader reader = new StreamReader(body, encoding);

                //Deserializza il JSON decodificato nella classe Packet
                var json = JsonConvert.DeserializeObject<Packet>(reader.ReadToEnd());
                Debug.WriteLine(json.ToString());
                //ProcessPacket processa la richiesta:
                //  1. Risponde a richieste di ID dalle board
                //  2. Risponde a richiesta di TIME_STAMP dalle board
                //  3. Salva i dati ricevuti dalle board nel DB
                PacketHandler.ProcessPacket(json, context);
                Debug.WriteLine("packet processed");
            }
            catch (Exception e)
            {
                //TODO Stampa su log4net
                Debug.WriteLine(e.Message);
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            //Click on button_computeLongTermStats
            //When click on this button, Compute the LongTermStats 
            DateTime from = dateTimePicker1_from.Value;
            DateTime to = dateTimePicker_to.Value;

            double from_timestamp = DateTimeToUnixTimestamp(from);
            double to_timestamp = DateTimeToUnixTimestamp(to);

            if (!(from_timestamp < to_timestamp))
            {
                MessageBox.Show("'From' date is larger than 'To' date. Retry.", "Uncorrect dates");
                return;
            }
            ///////QUERY che ritorna una lista di item: [mac,#occorrenze,prima_volta,ultima_volta]



            //inizializza la tabella
            var dt = new DataTable();
            dt.Columns.Add("Mac");
            dt.Columns.Add("Occurrencies");
            dt.Columns.Add("FirstlySeen");
            dt.Columns.Add("LastlySeen");

            if (connectionIsPossible)
            {
                var deviceStats = DeviceStatistics.CountDevice(Convert.ToInt32(from_timestamp), Convert.ToInt32(to_timestamp));
                foreach (var devStat in deviceStats)
                {
                    string mac_ds = devStat.Mac;
                    string occurrencies_ds = " "+devStat.Counter;
                    string ts_start_ds = UnixTimeStampToDateTime(devStat.Timestamp_start).ToString();
                    string ts_end_ds = UnixTimeStampToDateTime(devStat.Timestamp_end).ToString();
                    //aggiungi una riga alla tabella:
                    dt.Rows.Add(mac_ds, occurrencies_ds, ts_start_ds, ts_end_ds);

                }
            } 
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    //questa parte è solo per debug, si può sostituire con il foreach che aggiunge una Row per ogni elemento
                    dt.Rows.Add("MAC1", "22", from.ToString(), to.ToString());
                    dt.Rows.Add("MAC2", "24", "primavolta2", "secondavolta2");
                }
            }


            dataGridView_Statistics.DataSource = dt;
        }


        private void Form_Home_Load(object sender, EventArgs e)
        {

            if (connectionIsPossible)
            {
                Thread server = new Thread(new ThreadStart(AsyncServer));
                server.Start();

                Debug.WriteLine(server.IsAlive);
            }
            GenerateGraph();
        }


        //GenerateGraph():: create the INITIAL structure of the chart, and then with PlotGraph, it is filled up with the values.
        private void GenerateGraph()
        {
            double maxX = boards[0].x;
            double maxY = boards[0].y;
            boards.ForEach(o =>
            {
                if (o.x >= maxX) maxX = o.x;
                if (o.y >= maxY) maxY = o.y;
            });

            chart_Map.Series.Clear();
            var ss = new Series();
            //ss.Name = label8.Text;
            ss.ChartArea = "ChartArea1";
            ss.ChartType = SeriesChartType.Point;
            ss.Legend = "Legend1";
            chart_Map.Legends.Clear();
            ss.XValueType = ChartValueType.Double;
            ss.YValueType = ChartValueType.Double;
            chart_Map.ChartAreas[0].BackColor = Color.WhiteSmoke;
            chart_Map.ChartAreas[0].AxisX.Minimum = -0.5;
            chart_Map.ChartAreas[0].AxisX.Maximum = maxX + 0.5;
            chart_Map.ChartAreas[0].AxisX.LabelStyle.Interval = 0.5;
            chart_Map.ChartAreas[0].AxisY.Minimum = -0.5;
            chart_Map.ChartAreas[0].AxisY.Maximum = maxY + 0.5;
            chart_Map.ChartAreas[0].AxisY.LabelStyle.Interval = 0.5;
            chart_Map.ChartAreas[0].AxisX.MinorGrid.Interval = 0.1;
            chart_Map.ChartAreas[0].AxisY.MinorGrid.Interval = 0.1;
            chart_Map.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
            chart_Map.ChartAreas[0].AxisY.MinorGrid.Enabled = true;
            chart_Map.ChartAreas[0].AxisX.MinorGrid.LineDashStyle = ChartDashStyle.Dot;
            chart_Map.ChartAreas[0].AxisY.MinorGrid.LineDashStyle = ChartDashStyle.Dot;
            chart_Map.ChartAreas[0].AxisX.MajorGrid.Interval = 0.5;
            chart_Map.ChartAreas[0].AxisY.MajorGrid.Interval = 0.5;
            //label11.Text = "m";
            //label12.Text = "m";
            //label12.BackColor = Color.White;
            //label11.Font = new Font(chart1.ChartAreas[0].AxisX.LabelStyle.Font.Name, 10);
            //label12.Font = new Font(chart1.ChartAreas[0].AxisX.LabelStyle.Font.Name, 10);

            //trackBar_periodMap.Visible = true;

            this.chart_Map.Series.Add(ss);
            this.chart_Map.Series[ss.Name].IsValueShownAsLabel = true;

            boards.ForEach(o =>
            {
                Debug.WriteLine("Device: " + o.id + " trovato");
                DataPoint dp1 = new DataPoint();
                dp1.SetValueXY(o.x, o.y);
                dp1.Font = new Font("Arial", 10, FontStyle.Bold);
                dp1.Label = "ESP32 " + o.id;
                ss.Points.Add(dp1);
            });
            
            
            
            
            chart_Map.Invalidate();
        }

        public void RefreshMacPeriodsGraph() {

            chart_macOccurenciesPerPeriod.Series["Series1"].ChartArea = "ChartArea1";
            chart_macOccurenciesPerPeriod.Series["Series1"].XValueType = ChartValueType.Int32;
            chart_macOccurenciesPerPeriod.Series["Series1"].YValueType = ChartValueType.Int32;
            chart_macOccurenciesPerPeriod.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
            chart_macOccurenciesPerPeriod.ChartAreas["ChartArea1"].AxisY.Maximum = 100;
            chart_macOccurenciesPerPeriod.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chart_macOccurenciesPerPeriod.Legends.Clear();
            chart_macOccurenciesPerPeriod.ChartAreas["ChartArea1"].BackColor = Color.WhiteSmoke;
            chart_macOccurenciesPerPeriod.Series["Series1"].Color = Color.DarkRed;

            statsBeginningLabel.Text = "Started at:" + startUpDateTime.ToString();


            chart_macOccurenciesPerPeriod.Series["Series1"].Points.Clear();

            //DataPoint dp2 = new DataPoint();
            //riempimento del grafico
            foreach (int counts_stat in counts_stats)
            {
                //dp2.SetValueXY(i, counts_stats[i]);
                chart_macOccurenciesPerPeriod.Series["Series1"].Points.AddY(counts_stat);
            }

            label4.Text = "Ultimo aggiornamento " + DateTime.Now.ToString();
            chart_macOccurenciesPerPeriod.Invalidate();
        }


        private async void Timer_Tick(object sender, EventArgs e)
        {
           
            visualizedInterval = new MinuteInterval(DateTime.Now);
            LaunchMapRefresh(checkBox1.Checked, true, true); //in live mode faccio le stime delle posizioni minuto per minuto, anche se la casella non è spuntata
        } 

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                   new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void MoveRight_Click(object sender, EventArgs e)
        {
            //int moveUnit = GetValueFromTimeUnit(); //il valore che va sommato al timestamp iniziale
            //Debug.WriteLine(moveUnit.ToString());
        }

        private int GetValueFromTimeUnit()
        {
            //Ritorna il valore dello spostamento tramite freccia, in secondi.
            string timeunit = comboBox_TimeUnit.Text.ToLower();
            Debug.WriteLine(timeunit);
            int unit = 1;
            switch (timeunit)
            {
                case "seconds":
                    //already aligned
                    break;

                case "minutes":
                    unit *= 60;
                    break;

                case "hours":
                    unit *= 60 * 60;
                    break;

                case "days":
                    unit *= 60 * 60 * 24;
                    break;

                case "weeks":
                    unit *= 60 * 60 * 24 * 7;
                    break;

                case "months":
                    unit *= 60 * 60 * 24 * 30;
                    break;

                case "years":
                    unit *= 60 * 60 * 24 * 365;
                    break;


                default:
                    break;
            }
            Debug.WriteLine("Timeunit->" + unit);
            return unit;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            //Go to Now. Put the timepicker on current time
            timepicker_map.Value = DateTime.Now;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            // CHECKBOX 1 == LIVE MODE

            if (checkBox1.Checked)
            {
                groupBox_map1.Enabled = false;
                groupBox_map2.Enabled = false;
                //timer.Enabled = true;
            }
            else
            {
                groupBox_map1.Enabled = true;
                groupBox_map2.Enabled = true;
                //timer.Enabled = false;
            }
        }

        private void PlusOneUnit_Click(object sender, EventArgs e)
        {
            // PRENDE IL CURRENT MOMENT, AGGIUNGE UN UNITà E LANCIA L'AGGIORNAMENTO DELLA GUI
            int unit = GetValueFromTimeUnit();
            Debug.WriteLine("PlusOneUnit-->" + unit);
            visualizedInterval.add(unit);
            LaunchMapRefresh(true, false, false);

        }

        private void LessOneUnit_Click(object sender, EventArgs e)
        {
            // PRENDE IL CURRENT MOMENT, SOTTRAE UN UNITà E LANCIA L'AGGIORNAMENTO DELLA GUI
            int unit = GetValueFromTimeUnit();
            visualizedInterval.subtract(unit);
            LaunchMapRefresh(true, false, false);

        }

        public void LaunchMapRefresh(bool grafHasToBeRefreshed, bool statsHasToBeRefreshed, bool estimation) {
            
            // FUNZIONE PRINCIPALE CHE INNESTA QUERY + AGGIORNAMENTO_GUI.
            //Parametri:
            //  graphHasToBeRefreshed, = true se deve aggiornare la gui, false se invece deve solo effettuare la query (per il timer_tick quando non si è in live_mode)
            //  statsHasToBeRefreshed, = true se devi aggiornare anche le statistiche di lungo periodo (finestra 2), false altrimenti 

            //aggiorno le label dell'ultimo aggiornamento
            label3.Text = "Ultimo aggiornamento " + DateTime.Now.ToString();

            //Inizializzo l'oggetto devices. Conterrà i dispositivi trovati nel visualizedInterval.
            List<DevicePosition> devices;

            //DEBUG -> fai la query solo se ConnectionisPossible
            if (connectionIsPossible)
            {
                if (estimation == true)
                {
                    devices = positionHandler.GetPositions((Int32)new MinuteInterval(DateTime.Now).getFromUnixTimestamp(), 0.30, estimation); //devices è un array di List -> 1 elemento: standard - 2 elemento: hidden
                                                                                                                                              //devices = positionHandler.EstimateNotHiddenPositions(timestamp_from);
                }
                else
                {
                    devices = positionHandler.GetPositions((Int32)visualizedInterval.getFromUnixTimestamp(), 0.30, estimation); //devices è un array di List -> 1 elemento: standard - 2 elemento: hidden
                }
            }
             else
            {
                //devices = new List<DevicePosition>[1]; //FOR DEBUG
            }
                

            int occurrencies = devices.Count; //Conteggio dei dispositivi per la statistica



            if (statsHasToBeRefreshed)
            {
                if (connectionIsPossible) counts_stats.Add(occurrencies); //nel caso in cui il grafico vada aggiornato, prendi anche il conteggio
                else
                {
                    Random random = new Random();
                    counts_stats.Add(random.Next(100));
                }
            }
            if (grafHasToBeRefreshed) this.GenerateGraph(); //refresh graph only in case live mode is enabled
            if (statsHasToBeRefreshed) this.RefreshMacPeriodsGraph(); //refresh del grafico in tabella due solo se la chiamata è relativa al timertick

            if (!(devices[0] is null))
            {
                log.Info("numero device trovati: " + devices.Count());
            }

            if (!(checkBox1.Checked)) {
                textBox_currentMoment.Text = visualizedInterval.from.ToString() +" -> "+visualizedInterval.to.TimeOfDay.ToString().Split('.')[0];
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //COMPUTE BUTTON
            //QUANDO CLICCHI, IL VISUALIZED MINUTE è POSIZIONATO SUL DATETIME SCELTO E VIENE AGGIORNATA LA GUI
            visualizedInterval = new MinuteInterval(timepicker_map.Value);
            LaunchMapRefresh(true, false, false);
        }

        private void Timepicker_map_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Button_ltstats_from_now_Click(object sender, EventArgs e)
        {
            dateTimePicker1_from.Value = DateTime.Now;
        }

        private void Button_ltstats_to_now_Click(object sender, EventArgs e)
        {
            dateTimePicker_to.Value = DateTime.Now;
        }
        
        
        
        
        //////////----------- HASH FUNCTIONS ------------///////////
        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        private static bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            // Hash the input.
            var hashOfInput = GetHash(hashAlgorithm, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }

        //////////----------- END HASH FUNCTIONS ------------///////////

        
    }
}

