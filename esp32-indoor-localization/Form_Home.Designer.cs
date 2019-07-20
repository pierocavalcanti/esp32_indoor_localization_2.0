using System;
using System.Windows.Forms;

namespace esp32_indoor_localization
{
    partial class Form_Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Button button1;
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_mapView = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.timepicker_map = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_currentMoment = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox_TimeUnit = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chart_Map = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage_DevicePer5min = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.chart_macOccurenciesPerPeriod = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage_LongTermStatistics = new System.Windows.Forms.TabPage();
            this.dataGridView_Statistics = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1_from = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.button_computeMacPerPeriod = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker_to = new System.Windows.Forms.DateTimePicker();
            this.timer = new System.Windows.Forms.Timer(this.components);
            button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage_mapView.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Map)).BeginInit();
            this.tabPage_DevicePer5min.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_macOccurenciesPerPeriod)).BeginInit();
            this.tabPage_LongTermStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Statistics)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_mapView);
            this.tabControl1.Controls.Add(this.tabPage_DevicePer5min);
            this.tabControl1.Controls.Add(this.tabPage_LongTermStatistics);
            this.tabControl1.Location = new System.Drawing.Point(8, 3);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(891, 445);
            this.tabControl1.TabIndex = 0;
            //
            // tabPage_mapView
            //
            this.tabPage_mapView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage_mapView.Controls.Add(this.groupBox2);
            this.tabPage_mapView.Controls.Add(this.groupBox3);
            this.tabPage_mapView.Controls.Add(this.label3);
            this.tabPage_mapView.Controls.Add(this.chart_Map);
            this.tabPage_mapView.Location = new System.Drawing.Point(4, 22);
            this.tabPage_mapView.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage_mapView.Name = "tabPage_mapView";
            this.tabPage_mapView.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage_mapView.Size = new System.Drawing.Size(883, 419);
            this.tabPage_mapView.TabIndex = 0;
            this.tabPage_mapView.Text = "Map";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.timepicker_map);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Location = new System.Drawing.Point(530, 16);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(230, 125);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "TimePeriod Definition";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(13, 76);
            this.button4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(89, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "Go to now";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // timepicker_map
            // 
            this.timepicker_map.Location = new System.Drawing.Point(13, 37);
            this.timepicker_map.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.timepicker_map.Name = "timepicker_map";
            this.timepicker_map.Size = new System.Drawing.Size(199, 20);
            this.timepicker_map.TabIndex = 2;
            this.timepicker_map.Value = new System.DateTime(2019, 7, 14, 19, 59, 17, 538);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 21);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "From:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(120, 76);
            this.button3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Compute";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(button1);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.textBox_currentMoment);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.comboBox_TimeUnit);
            this.groupBox3.Location = new System.Drawing.Point(530, 145);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(232, 174);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "TimePeriod Control";
            this.groupBox3.UseWaitCursor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(163, 106);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(47, 32);
            this.button2.TabIndex = 4;
            this.button2.Text = "▶";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.UseWaitCursor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 58);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Current Visualization:";
            this.label7.UseWaitCursor = true;
            // 
            // textBox_currentMoment
            // 
            this.textBox_currentMoment.Location = new System.Drawing.Point(15, 73);
            this.textBox_currentMoment.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_currentMoment.Name = "textBox_currentMoment";
            this.textBox_currentMoment.ReadOnly = true;
            this.textBox_currentMoment.Size = new System.Drawing.Size(197, 20);
            this.textBox_currentMoment.TabIndex = 2;
            this.textBox_currentMoment.UseWaitCursor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 31);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Time Unit:";
            this.label8.UseWaitCursor = true;
            // 
            // comboBox_TimeUnit
            // 
            this.comboBox_TimeUnit.FormattingEnabled = true;
            this.comboBox_TimeUnit.Items.AddRange(new object[] {
            "Minutes",
            "Hours",
            "Days",
            "Months"});
            this.comboBox_TimeUnit.Location = new System.Drawing.Point(107, 31);
            this.comboBox_TimeUnit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox_TimeUnit.Name = "comboBox_TimeUnit";
            this.comboBox_TimeUnit.Size = new System.Drawing.Size(105, 21);
            this.comboBox_TimeUnit.TabIndex = 0;
            this.comboBox_TimeUnit.UseWaitCursor = true;
            // 
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.GreenYellow;
            this.label3.Location = new System.Drawing.Point(529, 377);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            //
            // chart_Map
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_Map.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_Map.Legends.Add(legend1);
            this.chart_Map.Location = new System.Drawing.Point(6, 16);
            this.chart_Map.Name = "chart_Map";
            this.chart_Map.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_Map.Series.Add(series1);
            this.chart_Map.Size = new System.Drawing.Size(519, 374);
            this.chart_Map.TabIndex = 0;
            this.chart_Map.Text = "chart1";
            this.chart_Map.Click += new System.EventHandler(this.Chart_Map_Click);
            //
            // tabPage_DevicePer5min
            //
            this.tabPage_DevicePer5min.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage_DevicePer5min.Controls.Add(this.label4);
            this.tabPage_DevicePer5min.Controls.Add(this.chart_macOccurenciesPerPeriod);
            this.tabPage_DevicePer5min.Location = new System.Drawing.Point(4, 22);
            this.tabPage_DevicePer5min.Name = "tabPage_DevicePer5min";
            this.tabPage_DevicePer5min.Size = new System.Drawing.Size(883, 419);
            this.tabPage_DevicePer5min.TabIndex = 2;
            this.tabPage_DevicePer5min.Text = "Device per min Statistics";
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Chartreuse;
            this.label4.Location = new System.Drawing.Point(3, 408);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "label4";
            this.label4.Click += new System.EventHandler(this.Label4_Click);
            //
            // chart_macOccurenciesPerPeriod
            // 
            chartArea2.Name = "ChartArea1";
            this.chart_macOccurenciesPerPeriod.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart_macOccurenciesPerPeriod.Legends.Add(legend2);
            this.chart_macOccurenciesPerPeriod.Location = new System.Drawing.Point(87, 19);
            this.chart_macOccurenciesPerPeriod.Name = "chart_macOccurenciesPerPeriod";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart_macOccurenciesPerPeriod.Series.Add(series2);
            this.chart_macOccurenciesPerPeriod.Size = new System.Drawing.Size(703, 374);
            this.chart_macOccurenciesPerPeriod.TabIndex = 1;
            this.chart_macOccurenciesPerPeriod.Text = "chart2";
            //
            // tabPage_LongTermStatistics
            //
            this.tabPage_LongTermStatistics.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage_LongTermStatistics.Controls.Add(this.dataGridView_Statistics);
            this.tabPage_LongTermStatistics.Controls.Add(this.groupBox1);
            this.tabPage_LongTermStatistics.Location = new System.Drawing.Point(4, 22);
            this.tabPage_LongTermStatistics.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage_LongTermStatistics.Name = "tabPage_LongTermStatistics";
            this.tabPage_LongTermStatistics.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage_LongTermStatistics.Size = new System.Drawing.Size(883, 419);
            this.tabPage_LongTermStatistics.TabIndex = 1;
            this.tabPage_LongTermStatistics.Text = "LongTermStatistics";
            // 
            // dataGridView_Statistics
            // 
            this.dataGridView_Statistics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Statistics.Location = new System.Drawing.Point(27, 20);
            this.dataGridView_Statistics.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView_Statistics.Name = "dataGridView_Statistics";
            this.dataGridView_Statistics.ReadOnly = true;
            this.dataGridView_Statistics.RowHeadersWidth = 100;
            this.dataGridView_Statistics.RowTemplate.Height = 28;
            this.dataGridView_Statistics.Size = new System.Drawing.Size(502, 363);
            this.dataGridView_Statistics.TabIndex = 9;
            // 
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.dateTimePicker1_from);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button_computeMacPerPeriod);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimePicker_to);
            this.groupBox1.Location = new System.Drawing.Point(593, 20);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(230, 162);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TimePeriod Definition";
            //
            // dateTimePicker1_from
            // 
            this.dateTimePicker1_from.Location = new System.Drawing.Point(23, 39);
            this.dateTimePicker1_from.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dateTimePicker1_from.Name = "dateTimePicker1_from";
            this.dateTimePicker1_from.Size = new System.Drawing.Size(189, 20);
            this.dateTimePicker1_from.TabIndex = 2;
            this.dateTimePicker1_from.Value = new System.DateTime(2019, 7, 14, 19, 59, 17, 538);
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "From:";
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            //
            // button_computeMacPerPeriod
            // 
            this.button_computeMacPerPeriod.Location = new System.Drawing.Point(39, 123);
            this.button_computeMacPerPeriod.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button_computeMacPerPeriod.Name = "button_computeMacPerPeriod";
            this.button_computeMacPerPeriod.Size = new System.Drawing.Size(143, 25);
            this.button_computeMacPerPeriod.TabIndex = 3;
            this.button_computeMacPerPeriod.Text = "Compute";
            this.button_computeMacPerPeriod.UseVisualStyleBackColor = true;
            this.button_computeMacPerPeriod.Click += new System.EventHandler(this.Button1_Click_1);
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "To:";
            //
            // dateTimePicker_to
            // 
            this.dateTimePicker_to.Location = new System.Drawing.Point(23, 83);
            this.dateTimePicker_to.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dateTimePicker_to.Name = "dateTimePicker_to";
            this.dateTimePicker_to.Size = new System.Drawing.Size(189, 20);
            this.dateTimePicker_to.TabIndex = 1;
            this.dateTimePicker_to.Value = new System.DateTime(2019, 7, 14, 19, 59, 17, 540);
            // 
            // timer
            //
            this.timer.Enabled = true;
            this.timer.Interval = 10000;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            //
            // Form_Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 456);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form_Home";
            this.Text = "Indoor Localization System";
            this.Load += new System.EventHandler(this.Form_Home_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_mapView.ResumeLayout(false);
            this.tabPage_mapView.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Map)).EndInit();
            this.tabPage_DevicePer5min.ResumeLayout(false);
            this.tabPage_DevicePer5min.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_macOccurenciesPerPeriod)).EndInit();
            this.tabPage_LongTermStatistics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Statistics)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_mapView;
        private System.Windows.Forms.TabPage tabPage_LongTermStatistics;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Map;
        private System.Windows.Forms.Button button_computeMacPerPeriod;
        private System.Windows.Forms.DateTimePicker dateTimePicker1_from;
        private System.Windows.Forms.DateTimePicker dateTimePicker_to;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TabPage tabPage_DevicePer5min;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_macOccurenciesPerPeriod;
        private Label label3;
        private Label label4;
        private GroupBox groupBox1;
        private DataGridView dataGridView_Statistics;
        private GroupBox groupBox3;
        private Button button2;
        private Label label7;
        private TextBox textBox_currentMoment;
        private Label label8;
        private ComboBox comboBox_TimeUnit;
        private GroupBox groupBox2;
        private DateTimePicker timepicker_map;
        private Label label5;
        private Button button3;
        private Button button4;
    }


}
