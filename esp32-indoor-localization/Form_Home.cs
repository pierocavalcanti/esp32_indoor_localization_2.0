using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using log4net;
using System.Reflection;

namespace esp32_indoor_localization
{
    public partial class Form_Home : Form
    {

        private const string IP_ADDRESS_SERVER = "http://192.168.1.16:3000/";
        private static readonly ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        System.Windows.Forms.Timer t;
        List<BoardLoader.Board> boards;
        //List<DevicePosition> devices;
        PositionHandler positionHandler;
        DeviceStatistics deviceStatistics = new DeviceStatistics();
        static DateTime startUpDateTime = DateTime.Now; //momento di apertura del programma per StatsChart

        public static double threshold=0.30;

        //VISUALIZED INTERVAL INDICA IL MINUTO CHE è VISUALIZZATO NELLA MAPPA
        MinuteInterval visualizedInterval = new MinuteInterval(DateTime.Now);

        //PER CONTEGGIO DISPOSITIVI OGNI MINUTO:
        List<int> counts_stats = new List<int>();

        public Form_Home()
        {
            //Inizializza form
            InitializeComponent();

            //Crea un nuovo Thread separato per il server (while true loop che genera un Thread ad ogni richiesta)
            t = new System.Windows.Forms.Timer();

            //Leggo il file di config delle boards e accedo alla lista delle board. BoardLoader classe Singleton
            boards = BoardLoader.Instance.Boards;
            positionHandler = new PositionHandler();

            //fixSize();
            //visualizedInterval = new MinuteInterval(DateTime.Now);

            counts_stats = new List<int>();
        }

        private void Form_Home_Load(object sender, EventArgs e)
        {
            try
            {
                Thread server = new Thread(new ThreadStart(AsyncServer));
                server.Start();
                server.IsBackground = true;

                RefreshMap(new List<DevicePosition>());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AsyncServer()
        {
            try
            {
                //Server HTTP asincrono
                var listener = new HttpListener();
                listener.Prefixes.Add(IP_ADDRESS_SERVER);
                listener.Start();

                while (true)
                {
                    //Genera un thread ad ogni richiesta
                    var context = listener.GetContext();
                    bool esiste = ThreadPool.QueueUserWorkItem(o => HandleRequest(context));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (checkBox_LiveMode.Checked)
            {
                // LiveMode is On
                LiveMode();
            }
            else {
                Tick_on_HistoryMode();
            }
        }

        private void Timer_computePositions_Tick(object sender, EventArgs e)
        {
            Thread computePositions = new Thread(new ThreadStart(positionHandler.EstimateNotHiddenPositions));
            computePositions.Start();
            computePositions.IsBackground = true;
            Thread computePositionsHidden = new Thread(new ThreadStart(positionHandler.EstimateHiddenPositions));
            computePositionsHidden.Start();
            computePositionsHidden.IsBackground = true;
            if (this.timer_computePositions.Interval == 30000)
            {
                this.timer_computePositions.Interval = 60000;
                this.timer_computePositions.Enabled = true;
                Debug.WriteLine("Timer impostato a 30 sec");
            }
            else Debug.WriteLine("Sono gia a sessanta");
        }

        private void Tick_on_HistoryMode() {
            var positions = QueryPositions(false);
            RefreshStats(positions.Count);
        }

        private void LiveMode() {
            var positions = QueryPositions(false);
            RefreshStats(positions.Count);
            RefreshMap(positions);
        }

        private void HistoryMode() {
            var positions = QueryPositions(true);
            RefreshMap(positions);

            if (!(checkBox_LiveMode.Checked))
            {
                textBox_currentMoment.Text = visualizedInterval.from.ToString() + " -> " + visualizedInterval.to.TimeOfDay.ToString().Split('.')[0];
            }
        }

        private List<DevicePosition> QueryPositions(bool getFromCache)
        {
            //Inizializzo l'oggetto devices. Conterrà i dispositivi trovati nel visualizedInterval.
            List<DevicePosition> devices;
            Int32 timestamp;
            if (getFromCache) timestamp = (Int32)visualizedInterval.getFromUnixTimestamp();
            else timestamp = (Int32) DateTimeToUnixTimestamp(DateTime.Now)-60;
            devices = positionHandler.GetPositions(timestamp, 0.30);

            label3.Text = "Ultimo aggiornamento " + DateTime.Now.ToString();

            return devices;
        }

        private void RefreshMap(List<DevicePosition> positions) {
            double maxX = boards[0].x;
            double maxY = boards[0].y;
            boards.ForEach(o =>
            {
                if (o.x >= maxX) maxX = o.x;
                if (o.y >= maxY) maxY = o.y;
            });

            chart_Map.Series.Clear();
            var ss = new Series();
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

            this.chart_Map.Series.Add(ss);
            this.chart_Map.Series[ss.Name].IsValueShownAsLabel = true;

            boards.ForEach(o =>
            {
                DataPoint dp1 = new DataPoint();
                dp1.SetValueXY(o.x, o.y);
                dp1.Font = new Font("Arial", 10, FontStyle.Bold);
                dp1.Label = "ESP32 " + o.id;
                ss.Points.Add(dp1);
            });

            if (positions != null)
            {
                positions.ForEach(o =>
                {
                    DataPoint dp1 = new DataPoint();
                    dp1.SetValueXY(o.X, o.Y);
                    dp1.Font = new Font("Arial", 10, FontStyle.Bold);
                    dp1.Label = o.Mac;
                    ss.Points.Add(dp1);
                });
            }

            chart_Map.Invalidate();
        }

        private void RefreshStats(int occurrencies)
        {
            counts_stats.Add(occurrencies);

            chart_macOccurenciesPerPeriod.Series["Series1"].ChartArea = "ChartArea1";
            chart_macOccurenciesPerPeriod.Series["Series1"].XValueType = ChartValueType.Int32;
            chart_macOccurenciesPerPeriod.Series["Series1"].YValueType = ChartValueType.Int32;
            chart_macOccurenciesPerPeriod.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
            int maxvalue = 10;
            if (counts_stats != null && counts_stats.Max() > maxvalue) maxvalue = counts_stats.Max();
            chart_macOccurenciesPerPeriod.ChartAreas["ChartArea1"].AxisY.Maximum = maxvalue;
            chart_macOccurenciesPerPeriod.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chart_macOccurenciesPerPeriod.Legends.Clear();
            chart_macOccurenciesPerPeriod.ChartAreas["ChartArea1"].BackColor = Color.WhiteSmoke;
            chart_macOccurenciesPerPeriod.Series["Series1"].Color = Color.DarkRed;

            statsBeginningLabel.Text = "Started at:" + startUpDateTime.ToString();

            chart_macOccurenciesPerPeriod.Series["Series1"].Points.Clear();

            //riempimento del grafico
            foreach (int counts_stat in counts_stats)
            {
                chart_macOccurenciesPerPeriod.Series["Series1"].Points.AddY(counts_stat);
            }

            label4.Text = "Ultimo aggiornamento " + DateTime.Now.ToString();
            chart_macOccurenciesPerPeriod.Invalidate();
        }

        class MinuteInterval
        {
            // Classe che descrive un minuto, in modo da poter usare la classe per gestire l'intervallo visualizzato nella mappa
            // con le proprietà from e to accedi ai margini dell'intervallo
            // con add e subtract aggiungi il quantitativo di secondi, traslando l'intero intervallo
            // è possibile ottenere i due valore from e to in formato UnixTimeStamp
            public DateTime from { get; set; }
            public DateTime to { get; set; }

            public double getFromUnixTimestamp()
            {
                return DateTimeToUnixTimestamp(from);
            }
            public double getToUnixTimestamp()
            {
                return DateTimeToUnixTimestamp(to);
            }

            public MinuteInterval(DateTime val)
            {
                // costruttore parte dal minuto iniziale
                to = val;
                from = val.AddSeconds(-60);
            }

            public void add(Int64 val)
            {
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

        //Button_computeLongTermStats Click
        private void Button1_Click_1(object sender, EventArgs e)
        {
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

            //inizializza la tabella
            var dt = new DataTable();
            dt.Columns.Add("Mac");
            dt.Columns.Add("Occurrencies");
            dt.Columns["Occurrencies"].DataType = Type.GetType("System.Int32");
            dt.Columns.Add("FirstlySeen");
            dt.Columns.Add("LastlySeen");

            var deviceStats = DeviceStatistics.CountDevice(Convert.ToInt32(from_timestamp), Convert.ToInt32(to_timestamp));
            if (!(deviceStats is null))
            {
                foreach (var devStat in deviceStats)
                {
                    string mac_ds = devStat.Mac;
                    //string occurrencies_ds = " " + devStat.Counter;
                    int occurrencies_ds = devStat.Counter;
                    string ts_start_ds = UnixTimeStampToDateTime(devStat.Timestamp_start).ToString();
                    string ts_end_ds = UnixTimeStampToDateTime(devStat.Timestamp_end).ToString();
                    //aggiungi una riga alla tabella:
                    dt.Rows.Add(mac_ds, occurrencies_ds, ts_start_ds, ts_end_ds);
                }
            }
            dataGridView_Statistics.DataSource = dt;
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

            if (checkBox_LiveMode.Checked)
            {
                groupBox_map1.Enabled = false;
                groupBox_map2.Enabled = false;
            }
            else
            {
                groupBox_map1.Enabled = true;
                groupBox_map2.Enabled = true;
            }
        }

        private void PlusOneUnit_Click(object sender, EventArgs e)
        {
            // PRENDE IL CURRENT MOMENT, AGGIUNGE UN UNITà E LANCIA L'AGGIORNAMENTO DELLA GUI
            int unit = GetValueFromTimeUnit();
            visualizedInterval.add(unit);
            HistoryMode();
        }

        private void LessOneUnit_Click(object sender, EventArgs e)
        {
            // PRENDE IL CURRENT MOMENT, SOTTRAE UN UNITà E LANCIA L'AGGIORNAMENTO DELLA GUI
            int unit = GetValueFromTimeUnit();
            visualizedInterval.subtract(unit);
            HistoryMode();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //COMPUTE BUTTON
            //QUANDO CLICCHI, IL VISUALIZED MINUTE è POSIZIONATO SUL DATETIME SCELTO E VIENE AGGIORNATA LA GUI
            visualizedInterval = new MinuteInterval(timepicker_map.Value);
            HistoryMode();
        }

        private void Button_ltstats_from_now_Click(object sender, EventArgs e)
        {
            dateTimePicker1_from.Value = DateTime.Now;
        }

        private void Button_ltstats_to_now_Click(object sender, EventArgs e)
        {
            dateTimePicker_to.Value = DateTime.Now;
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            threshold = ((double)this.numericUpDown1.Value) / 100;
        }

        public static double getThreshold() {
            return threshold;
        }
        //private void setTimer(){
        //    var aTimer = new System.Timers.Timer(30000);
        //    aTimer.Elapsed += OnTimedEvent;
        //}

        //private static void OnTimedEvent(Object source, ElapsedEventArgs e) {

        //}

    }
}