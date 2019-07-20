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
            System.Windows.Forms.Button moveLeft;
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_mapView = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.chart_Map = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage_DevicePer5min = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.chart_macOccurenciesPerPeriod = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage_LongTermStatistics = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.moveRight = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_TimeUnit = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1_from = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.button_computeMacPerPeriod = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker_to = new System.Windows.Forms.DateTimePicker();
            this.listViewStatistics = new System.Windows.Forms.ListView();
            this.mac = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.occurencies = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.firstAppearance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lastAppearence = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer = new System.Windows.Forms.Timer(this.components);
            moveLeft = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage_mapView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Map)).BeginInit();
            this.tabPage_DevicePer5min.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_macOccurenciesPerPeriod)).BeginInit();
            this.tabPage_LongTermStatistics.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // moveLeft
            //
            moveLeft.Location = new System.Drawing.Point(57, 192);
            moveLeft.Name = "moveLeft";
            moveLeft.Size = new System.Drawing.Size(76, 63);
            moveLeft.TabIndex = 5;
            moveLeft.Text = "◀";
            moveLeft.UseVisualStyleBackColor = true;
            moveLeft.UseWaitCursor = true;
            //
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_mapView);
            this.tabControl1.Controls.Add(this.tabPage_DevicePer5min);
            this.tabControl1.Controls.Add(this.tabPage_LongTermStatistics);
            this.tabControl1.Location = new System.Drawing.Point(11, 4);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(2375, 1062);
            this.tabControl1.TabIndex = 0;
            //
            // tabPage_mapView
            //
            this.tabPage_mapView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage_mapView.Controls.Add(this.label3);
            this.tabPage_mapView.Controls.Add(this.chart_Map);
            this.tabPage_mapView.Location = new System.Drawing.Point(10, 48);
            this.tabPage_mapView.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tabPage_mapView.Name = "tabPage_mapView";
            this.tabPage_mapView.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tabPage_mapView.Size = new System.Drawing.Size(2355, 1004);
            this.tabPage_mapView.TabIndex = 0;
            this.tabPage_mapView.Text = "Map";
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.GreenYellow;
            this.label3.Location = new System.Drawing.Point(5, 628);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            //
            // chart_Map
            //
            chartArea3.Name = "ChartArea1";
            this.chart_Map.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart_Map.Legends.Add(legend3);
            this.chart_Map.Location = new System.Drawing.Point(130, 30);
            this.chart_Map.Margin = new System.Windows.Forms.Padding(4);
            this.chart_Map.Name = "chart_Map";
            this.chart_Map.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart_Map.Series.Add(series3);
            this.chart_Map.Size = new System.Drawing.Size(1055, 575);
            this.chart_Map.TabIndex = 0;
            this.chart_Map.Text = "chart1";
            this.chart_Map.Click += new System.EventHandler(this.Chart_Map_Click);
            //
            // tabPage_DevicePer5min
            //
            this.tabPage_DevicePer5min.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage_DevicePer5min.Controls.Add(this.label4);
            this.tabPage_DevicePer5min.Controls.Add(this.chart_macOccurenciesPerPeriod);
            this.tabPage_DevicePer5min.Location = new System.Drawing.Point(4, 29);
            this.tabPage_DevicePer5min.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage_DevicePer5min.Name = "tabPage_DevicePer5min";
            this.tabPage_DevicePer5min.Size = new System.Drawing.Size(2355, 1004);
            this.tabPage_DevicePer5min.TabIndex = 2;
            this.tabPage_DevicePer5min.Text = "Device per min Statistics";
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Chartreuse;
            this.label4.Location = new System.Drawing.Point(5, 628);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 32);
            this.label4.TabIndex = 2;
            this.label4.Text = "label4";
            this.label4.Click += new System.EventHandler(this.Label4_Click);
            //
            // chart_macOccurenciesPerPeriod
            //
            chartArea4.Name = "ChartArea1";
            this.chart_macOccurenciesPerPeriod.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart_macOccurenciesPerPeriod.Legends.Add(legend4);
            this.chart_macOccurenciesPerPeriod.Location = new System.Drawing.Point(130, 30);
            this.chart_macOccurenciesPerPeriod.Margin = new System.Windows.Forms.Padding(4);
            this.chart_macOccurenciesPerPeriod.Name = "chart_macOccurenciesPerPeriod";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chart_macOccurenciesPerPeriod.Series.Add(series4);
            this.chart_macOccurenciesPerPeriod.Size = new System.Drawing.Size(1055, 575);
            this.chart_macOccurenciesPerPeriod.TabIndex = 1;
            this.chart_macOccurenciesPerPeriod.Text = "chart2";
            //
            // tabPage_LongTermStatistics
            //
            this.tabPage_LongTermStatistics.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage_LongTermStatistics.Controls.Add(this.groupBox2);
            this.tabPage_LongTermStatistics.Controls.Add(this.groupBox1);
            this.tabPage_LongTermStatistics.Controls.Add(this.listViewStatistics);
            this.tabPage_LongTermStatistics.Location = new System.Drawing.Point(4, 29);
            this.tabPage_LongTermStatistics.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage_LongTermStatistics.Name = "tabPage_LongTermStatistics";
            this.tabPage_LongTermStatistics.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tabPage_LongTermStatistics.Size = new System.Drawing.Size(2355, 1004);
            this.tabPage_LongTermStatistics.TabIndex = 1;
            this.tabPage_LongTermStatistics.Text = "LongTermStatistics";
            //
            // groupBox2
            //
            this.groupBox2.Controls.Add(moveLeft);
            this.groupBox2.Controls.Add(this.moveRight);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.comboBox_TimeUnit);
            this.groupBox2.Location = new System.Drawing.Point(891, 317);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(343, 273);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "TimePeriod Control";
            this.groupBox2.UseWaitCursor = true;
            //
            // moveRight
            //
            this.moveRight.Location = new System.Drawing.Point(196, 192);
            this.moveRight.Name = "moveRight";
            this.moveRight.Size = new System.Drawing.Size(76, 63);
            this.moveRight.TabIndex = 4;
            this.moveRight.Text = "▶";
            this.moveRight.UseVisualStyleBackColor = true;
            this.moveRight.UseWaitCursor = true;
            //
            // label6
            //
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(159, 20);
            this.label6.TabIndex = 3;
            this.label6.Text = "Current Visualization:";
            this.label6.UseWaitCursor = true;
            this.label6.Click += new System.EventHandler(this.Label6_Click);
            //
            // textBox1
            //
            this.textBox1.Location = new System.Drawing.Point(22, 126);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(293, 26);
            this.textBox1.TabIndex = 2;
            this.textBox1.UseWaitCursor = true;
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "Time Unit:";
            this.label5.UseWaitCursor = true;
            this.label5.Click += new System.EventHandler(this.Label5_Click);
            //
            // comboBox_TimeUnit
            //
            this.comboBox_TimeUnit.FormattingEnabled = true;
            this.comboBox_TimeUnit.Items.AddRange(new object[] {
            "Minutes",
            "Hours",
            "Days",
            "Months"});
            this.comboBox_TimeUnit.Location = new System.Drawing.Point(133, 44);
            this.comboBox_TimeUnit.Name = "comboBox_TimeUnit";
            this.comboBox_TimeUnit.Size = new System.Drawing.Size(155, 28);
            this.comboBox_TimeUnit.TabIndex = 0;
            this.comboBox_TimeUnit.UseWaitCursor = true;
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.dateTimePicker1_from);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button_computeMacPerPeriod);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimePicker_to);
            this.groupBox1.Location = new System.Drawing.Point(889, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 250);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TimePeriod Definition";
            //
            // dateTimePicker1_from
            //
            this.dateTimePicker1_from.Location = new System.Drawing.Point(35, 60);
            this.dateTimePicker1_from.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker1_from.Name = "dateTimePicker1_from";
            this.dateTimePicker1_from.Size = new System.Drawing.Size(281, 26);
            this.dateTimePicker1_from.TabIndex = 2;
            this.dateTimePicker1_from.Value = new System.DateTime(2019, 7, 14, 19, 59, 17, 538);
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "From:";
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            //
            // button_computeMacPerPeriod
            //
            this.button_computeMacPerPeriod.Location = new System.Drawing.Point(58, 189);
            this.button_computeMacPerPeriod.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_computeMacPerPeriod.Name = "button_computeMacPerPeriod";
            this.button_computeMacPerPeriod.Size = new System.Drawing.Size(215, 39);
            this.button_computeMacPerPeriod.TabIndex = 3;
            this.button_computeMacPerPeriod.Text = "Compute";
            this.button_computeMacPerPeriod.UseVisualStyleBackColor = true;
            this.button_computeMacPerPeriod.Click += new System.EventHandler(this.Button1_Click_1);
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "To:";
            //
            // dateTimePicker_to
            //
            this.dateTimePicker_to.Location = new System.Drawing.Point(35, 127);
            this.dateTimePicker_to.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker_to.Name = "dateTimePicker_to";
            this.dateTimePicker_to.Size = new System.Drawing.Size(281, 26);
            this.dateTimePicker_to.TabIndex = 1;
            this.dateTimePicker_to.Value = new System.DateTime(2019, 7, 14, 19, 59, 17, 540);
            //
            // listViewStatistics
            //
            this.listViewStatistics.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.mac,
            this.occurencies,
            this.firstAppearance,
            this.lastAppearence});
            this.listViewStatistics.HideSelection = false;
            this.listViewStatistics.Location = new System.Drawing.Point(21, 31);
            this.listViewStatistics.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listViewStatistics.Name = "listViewStatistics";
            this.listViewStatistics.Size = new System.Drawing.Size(830, 559);
            this.listViewStatistics.TabIndex = 6;
            this.listViewStatistics.UseCompatibleStateImageBehavior = false;
            //
            // mac
            //
            this.mac.Text = "Mac";
            //
            // occurencies
            //
            this.occurencies.Text = "#";
            //
            // firstAppearance
            //
            this.firstAppearance.Text = "Firstly seen in:";
            //
            // lastAppearence
            //
            this.lastAppearence.Text = "Lastly seen in:";
            //
            // timer
            //
            this.timer.Enabled = true;
            this.timer.Interval = 10000;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            //
            // Form_Home
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1759, 844);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "Form_Home";
            this.Text = "Indoor Localization System";
            this.Load += new System.EventHandler(this.Form_Home_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_mapView.ResumeLayout(false);
            this.tabPage_mapView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Map)).EndInit();
            this.tabPage_DevicePer5min.ResumeLayout(false);
            this.tabPage_DevicePer5min.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_macOccurenciesPerPeriod)).EndInit();
            this.tabPage_LongTermStatistics.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.ListView listViewStatistics;
        private Label label3;
        private Label label4;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label5;
        private ComboBox comboBox_TimeUnit;
        private Label label6;
        private TextBox textBox1;
        private Button moveRight;
        private ColumnHeader mac;
        private ColumnHeader occurencies;
        private ColumnHeader firstAppearance;
        private ColumnHeader lastAppearence;
    }


}
