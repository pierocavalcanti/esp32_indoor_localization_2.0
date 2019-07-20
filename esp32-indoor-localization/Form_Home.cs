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
//using NLog;

namespace esp32_indoor_localization
{
    public partial class Form_Home : Form
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        System.Windows.Forms.Timer t;
         List<BoardLoader.Board> boards;
         List<DevicePosition> devices;
         PositionHandler positionHandler;

        private static string IP_ADDRESS_SERVER = "http://192.168.1.16:3000/";
        public Form_Home()
        {
            //Inizializza form
            InitializeComponent();
            //Crea un nuovo Thread separato per il server (while true loop che genera un Thread ad ogni richiesta)
            
            t = new System.Windows.Forms.Timer();


            //Leggo il file di config delle boards e accedo alla lista delle board. BoardLoader classe Singleton
            boards = BoardLoader.Instance.Boards;

            positionHandler = new PositionHandler();
            fixSize();

        }

        public void fixSize() {
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

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            //When click on this button, Compute the LongTermStatistics
            DateTime from = dateTimePicker1_from.Value;
            DateTime to = dateTimePicker_to.Value;

            double from_timestamp = DateTimeToUnixTimestamp(from);
            double to_timestamp = DateTimeToUnixTimestamp(to);

            ///////QUERY che ritorna una lista di item: [mac,#occorrenze,prima_volta,ultima_volta]



            
            var dt = new DataTable();
            dt.Columns.Add("Mac");
            dt.Columns.Add("Occurrencies");
            dt.Columns.Add("FirstlySeen");
            dt.Columns.Add("LastlySeen");

            //foreach(element in lista_ritornata_da_query)
            for (int i = 0; i < 10; i++) {
            dt.Rows.Add("MAC1", "22", from.ToString(), to.ToString());
            dt.Rows.Add("MAC2", "24", "primavolta2", "secondavolta2");

            } 

            dataGridView_Statistics.DataSource = dt;
            




            //conversion in UnixTimeStamp 


        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Form_Home_Load(object sender, EventArgs e)
        {
            GenerateGraph();

            // COMMENTARE BLOCCO QUANDO NON CONNESSO!!
            /*Thread server = new Thread(new ThreadStart(AsyncServer));
            server.Start();

            Debug.WriteLine(server.IsAlive);*/
            //Timer che ricarica chart_Map ogni minuto


            //t.Start();
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
                dp1.Label = "ESP32 "+o.id;
                ss.Points.Add(dp1);
            });

            if (devices != null)
            {
                devices.ForEach(o =>
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


        void PlotGraph(List<DevicePosition> devices) {

            chart_Map.Series.Clear();

            var ss = new Series();

            boards.ForEach(o =>
            {
                Debug.WriteLine("Device: " + o.id + " trovato");
                DataPoint dp1 = new DataPoint();
                dp1.SetValueXY(o.x, o.y);
                dp1.Font = new Font("Arial", 10, FontStyle.Bold);
                dp1.Label = "ESP32 " + o.id;
                ss.Points.Add(dp1);
            });

            if (devices != null)
            {
                devices.ForEach(o =>
                {

                    DataPoint dp1 = new DataPoint();
                    dp1.SetValueXY(o.X, o.Y);
                    dp1.Font = new Font("Arial", 10, FontStyle.Bold);
                    dp1.Label = o.Mac;
                    ss.Points.Add(dp1);
                });
            }

            chart_Map.Invalidate();

            //log.Info("numero device trovati: " + devices.Count);


        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //Funzione che richiama in ordine:
            //  GetPosition() restituisce la List di DevicePosition aggiornata

            
            //Int32 currentDate = (Int32) new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            Int32 unixTimeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Int32 timestamp_from = unixTimeStamp - 60;
            var current_hdate = UnixTimeStampToDateTime(unixTimeStamp);
            label3.Text = "Ultimo aggiornamento " + current_hdate.ToString();
            label4.Text = "Ultimo aggiornamento " + current_hdate.ToString();
            //Debug.WriteLine(this.getTrackBarValue().ToString() + "           " + unixTimestamp);


            //decommentare
            //devices = positionHandler.GetPositions(timestamp_from,0.30).Result; //bloccante



            Debug.WriteLine("boooooboboooobo");
            //  PlotGraph() plotta i devices sulla mappa

            this.GenerateGraph();

            //log.Info("numero device trovati: " + devices.Count);

        }

        //private void TrackBar_periodMap_Scroll(object sender, EventArgs e)
        //{
        //    //Funzione che richiama in ordine:
        //    //  GetPosition() restituisce la List di DevicePosition aggiornata
            
        //    Int32 timestamp_from = this.getTrackBarValue() * 60;
        //    //Int32 currentDate = (Int32) new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        //    Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        //    timestamp_from = unixTimestamp - timestamp_from;
        //    devices = positionHandler.GetPositions(timestamp_from,0.30).Result;
        //    //  GenerateGraph() disegna il chart della mappa con i dispositivi
        //    this.GenerateGraph();

        //}

        //public Int32 getTrackBarValue()
        //{
        //    return this.trackBar_periodMap.Value;
        //}

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
            int moveUnit = GetValueFromTimeUnit(); //il valore che va sommato al timestamp iniziale
            Debug.WriteLine(moveUnit.ToString());
        }

        private int GetValueFromTimeUnit() {
            string timeunit = comboBox_TimeUnit.Text;
            int unit = 1;
            switch (timeunit.ToLower()) {
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
            return unit;
        }

        private void MoveLeft_Click(object sender, EventArgs e)
        {
            int moveUnit = GetValueFromTimeUnit(); //il valore che va sommato al timestamp iniziale
            Debug.WriteLine(moveUnit.ToString());
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            timepicker_map.Value = DateTime.UtcNow;
        }
    }

}
