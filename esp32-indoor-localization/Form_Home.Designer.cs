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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend9 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea10 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend10 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_mapView = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox_map1 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.timepicker_map = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox_map2 = new System.Windows.Forms.GroupBox();
            this.lessOneUnit = new System.Windows.Forms.Button();
            this.plusOneUnit = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_currentMoment = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox_TimeUnit = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chart_Map = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage_DevicePer5min = new System.Windows.Forms.TabPage();
            this.statsBeginningLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chart_macOccurenciesPerPeriod = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage_LongTermStatistics = new System.Windows.Forms.TabPage();
            this.dataGridView_Statistics = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1_from = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.button_computeLongTermStats = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker_to = new System.Windows.Forms.DateTimePicker();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.button_ltstats_from_now = new System.Windows.Forms.Button();
            this.button_ltstats_to_now = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage_mapView.SuspendLayout();
            this.groupBox_map1.SuspendLayout();
            this.groupBox_map2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Map)).BeginInit();
            this.tabPage_DevicePer5min.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_macOccurenciesPerPeriod)).BeginInit();
            this.tabPage_LongTermStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Statistics)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_mapView);
            this.tabControl1.Controls.Add(this.tabPage_DevicePer5min);
            this.tabControl1.Controls.Add(this.tabPage_LongTermStatistics);
            this.tabControl1.Location = new System.Drawing.Point(12, 3);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1336, 685);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage_mapView
            // 
            this.tabPage_mapView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage_mapView.Controls.Add(this.checkBox1);
            this.tabPage_mapView.Controls.Add(this.groupBox_map1);
            this.tabPage_mapView.Controls.Add(this.groupBox_map2);
            this.tabPage_mapView.Controls.Add(this.label3);
            this.tabPage_mapView.Controls.Add(this.chart_Map);
            this.tabPage_mapView.Location = new System.Drawing.Point(4, 29);
            this.tabPage_mapView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage_mapView.Name = "tabPage_mapView";
            this.tabPage_mapView.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage_mapView.Size = new System.Drawing.Size(1328, 652);
            this.tabPage_mapView.TabIndex = 0;
            this.tabPage_mapView.Text = "Map";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(798, 25);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(107, 24);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Live Mode";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // groupBox_map1
            // 
            this.groupBox_map1.Controls.Add(this.button4);
            this.groupBox_map1.Controls.Add(this.timepicker_map);
            this.groupBox_map1.Controls.Add(this.label5);
            this.groupBox_map1.Controls.Add(this.button3);
            this.groupBox_map1.Enabled = false;
            this.groupBox_map1.Location = new System.Drawing.Point(795, 72);
            this.groupBox_map1.Name = "groupBox_map1";
            this.groupBox_map1.Size = new System.Drawing.Size(345, 192);
            this.groupBox_map1.TabIndex = 8;
            this.groupBox_map1.TabStop = false;
            this.groupBox_map1.Text = "TimePeriod Definition";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(20, 117);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(134, 35);
            this.button4.TabIndex = 10;
            this.button4.Text = "Go to now";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // timepicker_map
            // 
            this.timepicker_map.CustomFormat = "dd/MM/yyyy HH:mm";
            this.timepicker_map.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timepicker_map.Location = new System.Drawing.Point(20, 57);
            this.timepicker_map.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.timepicker_map.Name = "timepicker_map";
            this.timepicker_map.Size = new System.Drawing.Size(296, 26);
            this.timepicker_map.TabIndex = 2;
            this.timepicker_map.Value = new System.DateTime(2019, 7, 14, 19, 59, 17, 538);
            this.timepicker_map.ValueChanged += new System.EventHandler(this.Timepicker_map_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "From:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(180, 117);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(136, 35);
            this.button3.TabIndex = 3;
            this.button3.Text = "Compute";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // groupBox_map2
            // 
            this.groupBox_map2.Controls.Add(this.lessOneUnit);
            this.groupBox_map2.Controls.Add(this.plusOneUnit);
            this.groupBox_map2.Controls.Add(this.label7);
            this.groupBox_map2.Controls.Add(this.textBox_currentMoment);
            this.groupBox_map2.Controls.Add(this.label8);
            this.groupBox_map2.Controls.Add(this.comboBox_TimeUnit);
            this.groupBox_map2.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox_map2.Enabled = false;
            this.groupBox_map2.Location = new System.Drawing.Point(795, 271);
            this.groupBox_map2.Name = "groupBox_map2";
            this.groupBox_map2.Size = new System.Drawing.Size(348, 268);
            this.groupBox_map2.TabIndex = 9;
            this.groupBox_map2.TabStop = false;
            this.groupBox_map2.Text = "TimePeriod Control";
            // 
            // lessOneUnit
            // 
            this.lessOneUnit.Location = new System.Drawing.Point(22, 163);
            this.lessOneUnit.Name = "lessOneUnit";
            this.lessOneUnit.Size = new System.Drawing.Size(70, 49);
            this.lessOneUnit.TabIndex = 5;
            this.lessOneUnit.Text = "◀";
            this.lessOneUnit.UseVisualStyleBackColor = true;
            this.lessOneUnit.Click += new System.EventHandler(this.LessOneUnit_Click);
            // 
            // plusOneUnit
            // 
            this.plusOneUnit.Location = new System.Drawing.Point(244, 163);
            this.plusOneUnit.Name = "plusOneUnit";
            this.plusOneUnit.Size = new System.Drawing.Size(70, 49);
            this.plusOneUnit.TabIndex = 4;
            this.plusOneUnit.Text = "▶";
            this.plusOneUnit.UseVisualStyleBackColor = true;
            this.plusOneUnit.Click += new System.EventHandler(this.PlusOneUnit_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(159, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "Current Visualization:";
            // 
            // textBox_currentMoment
            // 
            this.textBox_currentMoment.Location = new System.Drawing.Point(22, 112);
            this.textBox_currentMoment.Name = "textBox_currentMoment";
            this.textBox_currentMoment.ReadOnly = true;
            this.textBox_currentMoment.Size = new System.Drawing.Size(294, 26);
            this.textBox_currentMoment.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 20);
            this.label8.TabIndex = 1;
            this.label8.Text = "Time Unit:";
            // 
            // comboBox_TimeUnit
            // 
            this.comboBox_TimeUnit.FormattingEnabled = true;
            this.comboBox_TimeUnit.Items.AddRange(new object[] {
            "Minutes",
            "Hours",
            "Days",
            "Months"});
            this.comboBox_TimeUnit.Location = new System.Drawing.Point(160, 48);
            this.comboBox_TimeUnit.Name = "comboBox_TimeUnit";
            this.comboBox_TimeUnit.Size = new System.Drawing.Size(156, 28);
            this.comboBox_TimeUnit.TabIndex = 0;
            this.comboBox_TimeUnit.SelectedIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Chartreuse;
            this.label3.Location = new System.Drawing.Point(794, 580);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            // 
            // chart_Map
            // 
            chartArea9.Name = "ChartArea1";
            this.chart_Map.ChartAreas.Add(chartArea9);
            legend9.Name = "Legend1";
            this.chart_Map.Legends.Add(legend9);
            this.chart_Map.Location = new System.Drawing.Point(9, 25);
            this.chart_Map.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chart_Map.Name = "chart_Map";
            this.chart_Map.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series9.ChartArea = "ChartArea1";
            series9.Legend = "Legend1";
            series9.Name = "Series1";
            this.chart_Map.Series.Add(series9);
            this.chart_Map.Size = new System.Drawing.Size(778, 575);
            this.chart_Map.TabIndex = 0;
            this.chart_Map.Text = "chart1";
            // 
            // tabPage_DevicePer5min
            // 
            this.tabPage_DevicePer5min.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage_DevicePer5min.Controls.Add(this.statsBeginningLabel);
            this.tabPage_DevicePer5min.Controls.Add(this.label4);
            this.tabPage_DevicePer5min.Controls.Add(this.chart_macOccurenciesPerPeriod);
            this.tabPage_DevicePer5min.Location = new System.Drawing.Point(4, 29);
            this.tabPage_DevicePer5min.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage_DevicePer5min.Name = "tabPage_DevicePer5min";
            this.tabPage_DevicePer5min.Size = new System.Drawing.Size(1328, 652);
            this.tabPage_DevicePer5min.TabIndex = 2;
            this.tabPage_DevicePer5min.Text = "Device per min Statistics";
            // 
            // statsBeginningLabel
            // 
            this.statsBeginningLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statsBeginningLabel.AutoSize = true;
            this.statsBeginningLabel.BackColor = System.Drawing.Color.Orange;
            this.statsBeginningLabel.Location = new System.Drawing.Point(15, 578);
            this.statsBeginningLabel.Margin = new System.Windows.Forms.Padding(2);
            this.statsBeginningLabel.Name = "statsBeginningLabel";
            this.statsBeginningLabel.Size = new System.Drawing.Size(51, 20);
            this.statsBeginningLabel.TabIndex = 3;
            this.statsBeginningLabel.Text = "label6";
            this.statsBeginningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Chartreuse;
            this.label4.Location = new System.Drawing.Point(845, 578);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "label4";
            this.label4.Click += new System.EventHandler(this.Label4_Click);
            // 
            // chart_macOccurenciesPerPeriod
            // 
            chartArea10.Name = "ChartArea1";
            this.chart_macOccurenciesPerPeriod.ChartAreas.Add(chartArea10);
            legend10.Name = "Legend1";
            this.chart_macOccurenciesPerPeriod.Legends.Add(legend10);
            this.chart_macOccurenciesPerPeriod.Location = new System.Drawing.Point(4, 29);
            this.chart_macOccurenciesPerPeriod.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chart_macOccurenciesPerPeriod.Name = "chart_macOccurenciesPerPeriod";
            series10.BorderWidth = 3;
            series10.ChartArea = "ChartArea1";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series10.Color = System.Drawing.Color.Red;
            series10.Legend = "Legend1";
            series10.Name = "Series1";
            this.chart_macOccurenciesPerPeriod.Series.Add(series10);
            this.chart_macOccurenciesPerPeriod.Size = new System.Drawing.Size(1205, 542);
            this.chart_macOccurenciesPerPeriod.TabIndex = 1;
            this.chart_macOccurenciesPerPeriod.Text = "chart2";
            // 
            // tabPage_LongTermStatistics
            // 
            this.tabPage_LongTermStatistics.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage_LongTermStatistics.Controls.Add(this.dataGridView_Statistics);
            this.tabPage_LongTermStatistics.Controls.Add(this.groupBox1);
            this.tabPage_LongTermStatistics.Location = new System.Drawing.Point(4, 29);
            this.tabPage_LongTermStatistics.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage_LongTermStatistics.Name = "tabPage_LongTermStatistics";
            this.tabPage_LongTermStatistics.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage_LongTermStatistics.Size = new System.Drawing.Size(1328, 652);
            this.tabPage_LongTermStatistics.TabIndex = 1;
            this.tabPage_LongTermStatistics.Text = "LongTermStatistics";
            // 
            // dataGridView_Statistics
            // 
            this.dataGridView_Statistics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Statistics.Location = new System.Drawing.Point(40, 31);
            this.dataGridView_Statistics.Name = "dataGridView_Statistics";
            this.dataGridView_Statistics.ReadOnly = true;
            this.dataGridView_Statistics.RowHeadersWidth = 100;
            this.dataGridView_Statistics.RowTemplate.Height = 28;
            this.dataGridView_Statistics.Size = new System.Drawing.Size(753, 558);
            this.dataGridView_Statistics.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_ltstats_to_now);
            this.groupBox1.Controls.Add(this.button_ltstats_from_now);
            this.groupBox1.Controls.Add(this.dateTimePicker1_from);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button_computeLongTermStats);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimePicker_to);
            this.groupBox1.Location = new System.Drawing.Point(800, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 249);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TimePeriod Definition";
            // 
            // dateTimePicker1_from
            // 
            this.dateTimePicker1_from.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dateTimePicker1_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1_from.Location = new System.Drawing.Point(34, 60);
            this.dateTimePicker1_from.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dateTimePicker1_from.Name = "dateTimePicker1_from";
            this.dateTimePicker1_from.Size = new System.Drawing.Size(187, 26);
            this.dateTimePicker1_from.TabIndex = 2;
            this.dateTimePicker1_from.Value = new System.DateTime(2019, 7, 14, 19, 59, 17, 538);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "From:";
            // 
            // button_computeLongTermStats
            // 
            this.button_computeLongTermStats.Location = new System.Drawing.Point(58, 189);
            this.button_computeLongTermStats.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.button_computeLongTermStats.Name = "button_computeLongTermStats";
            this.button_computeLongTermStats.Size = new System.Drawing.Size(214, 38);
            this.button_computeLongTermStats.TabIndex = 3;
            this.button_computeLongTermStats.Text = "Compute";
            this.button_computeLongTermStats.UseVisualStyleBackColor = true;
            this.button_computeLongTermStats.Click += new System.EventHandler(this.Button1_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "To:";
            // 
            // dateTimePicker_to
            // 
            this.dateTimePicker_to.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dateTimePicker_to.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_to.Location = new System.Drawing.Point(34, 128);
            this.dateTimePicker_to.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dateTimePicker_to.Name = "dateTimePicker_to";
            this.dateTimePicker_to.Size = new System.Drawing.Size(187, 26);
            this.dateTimePicker_to.TabIndex = 1;
            this.dateTimePicker_to.Value = new System.DateTime(2019, 7, 14, 19, 59, 17, 540);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 10000;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // button_ltstats_from_now
            // 
            this.button_ltstats_from_now.Location = new System.Drawing.Point(227, 58);
            this.button_ltstats_from_now.Name = "button_ltstats_from_now";
            this.button_ltstats_from_now.Size = new System.Drawing.Size(75, 35);
            this.button_ltstats_from_now.TabIndex = 6;
            this.button_ltstats_from_now.Text = "now";
            this.button_ltstats_from_now.UseVisualStyleBackColor = true;
            this.button_ltstats_from_now.Click += new System.EventHandler(this.Button_ltstats_from_now_Click);
            // 
            // button_ltstats_to_now
            // 
            this.button_ltstats_to_now.Location = new System.Drawing.Point(227, 125);
            this.button_ltstats_to_now.Name = "button_ltstats_to_now";
            this.button_ltstats_to_now.Size = new System.Drawing.Size(75, 37);
            this.button_ltstats_to_now.TabIndex = 7;
            this.button_ltstats_to_now.Text = "now";
            this.button_ltstats_to_now.UseVisualStyleBackColor = true;
            this.button_ltstats_to_now.Click += new System.EventHandler(this.Button_ltstats_to_now_Click);
            // 
            // Form_Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 702);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "Form_Home";
            this.Text = "Indoor Localization System";
            this.Load += new System.EventHandler(this.Form_Home_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_mapView.ResumeLayout(false);
            this.tabPage_mapView.PerformLayout();
            this.groupBox_map1.ResumeLayout(false);
            this.groupBox_map1.PerformLayout();
            this.groupBox_map2.ResumeLayout(false);
            this.groupBox_map2.PerformLayout();
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
        private System.Windows.Forms.Button button_computeLongTermStats;
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
        private GroupBox groupBox_map2;
        private Button plusOneUnit;
        private Label label7;
        private TextBox textBox_currentMoment;
        private Label label8;
        private ComboBox comboBox_TimeUnit;
        private GroupBox groupBox_map1;
        private DateTimePicker timepicker_map;
        private Label label5;
        private Button button3;
        private Button button4;
        private Button lessOneUnit;
        private CheckBox checkBox1;
        private Label statsBeginningLabel;
        private Button button_ltstats_to_now;
        private Button button_ltstats_from_now;
    }


}
