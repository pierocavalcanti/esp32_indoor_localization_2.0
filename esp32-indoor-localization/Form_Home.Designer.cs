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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_mapView = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.chart_Map = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage_DevicePer5min = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.chart_macOccurenciesPerPeriod = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage_LongTermStatistics = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_computeMacPerPeriod = new System.Windows.Forms.Button();
            this.dateTimePicker1_from = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_to = new System.Windows.Forms.DateTimePicker();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage_mapView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Map)).BeginInit();
            this.tabPage_DevicePer5min.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_macOccurenciesPerPeriod)).BeginInit();
            this.tabPage_LongTermStatistics.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_mapView);
            this.tabControl1.Controls.Add(this.tabPage_DevicePer5min);
            this.tabControl1.Controls.Add(this.tabPage_LongTermStatistics);
            this.tabControl1.Location = new System.Drawing.Point(14, 6);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
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
            this.label3.Location = new System.Drawing.Point(9, 938);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            // 
            // chart_Map
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_Map.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_Map.Legends.Add(legend1);
            this.chart_Map.Location = new System.Drawing.Point(5, 6);
            this.chart_Map.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.chart_Map.Name = "chart_Map";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_Map.Series.Add(series1);
            this.chart_Map.Size = new System.Drawing.Size(1876, 891);
            this.chart_Map.TabIndex = 0;
            this.chart_Map.Text = "chart1";
            this.chart_Map.Click += new System.EventHandler(this.Chart_Map_Click);
            // 
            // tabPage_DevicePer5min
            // 
            this.tabPage_DevicePer5min.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage_DevicePer5min.Controls.Add(this.label4);
            this.tabPage_DevicePer5min.Controls.Add(this.chart_macOccurenciesPerPeriod);
            this.tabPage_DevicePer5min.Location = new System.Drawing.Point(10, 48);
            this.tabPage_DevicePer5min.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tabPage_DevicePer5min.Name = "tabPage_DevicePer5min";
            this.tabPage_DevicePer5min.Size = new System.Drawing.Size(2355, 1004);
            this.tabPage_DevicePer5min.TabIndex = 2;
            this.tabPage_DevicePer5min.Text = "Device per min Statistics";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Chartreuse;
            this.label4.Location = new System.Drawing.Point(5, 953);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 32);
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
            this.chart_macOccurenciesPerPeriod.Location = new System.Drawing.Point(0, 6);
            this.chart_macOccurenciesPerPeriod.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.chart_macOccurenciesPerPeriod.Name = "chart_macOccurenciesPerPeriod";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart_macOccurenciesPerPeriod.Series.Add(series2);
            this.chart_macOccurenciesPerPeriod.Size = new System.Drawing.Size(1876, 891);
            this.chart_macOccurenciesPerPeriod.TabIndex = 1;
            this.chart_macOccurenciesPerPeriod.Text = "chart2";
            // 
            // tabPage_LongTermStatistics
            // 
            this.tabPage_LongTermStatistics.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage_LongTermStatistics.Controls.Add(this.listView1);
            this.tabPage_LongTermStatistics.Controls.Add(this.label2);
            this.tabPage_LongTermStatistics.Controls.Add(this.label1);
            this.tabPage_LongTermStatistics.Controls.Add(this.button_computeMacPerPeriod);
            this.tabPage_LongTermStatistics.Controls.Add(this.dateTimePicker1_from);
            this.tabPage_LongTermStatistics.Controls.Add(this.dateTimePicker_to);
            this.tabPage_LongTermStatistics.Location = new System.Drawing.Point(10, 48);
            this.tabPage_LongTermStatistics.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tabPage_LongTermStatistics.Name = "tabPage_LongTermStatistics";
            this.tabPage_LongTermStatistics.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tabPage_LongTermStatistics.Size = new System.Drawing.Size(2355, 1004);
            this.tabPage_LongTermStatistics.TabIndex = 1;
            this.tabPage_LongTermStatistics.Text = "LongTermStatistics";
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(116, 60);
            this.listView1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1465, 846);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1742, 288);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 32);
            this.label2.TabIndex = 5;
            this.label2.Text = "To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1742, 146);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 32);
            this.label1.TabIndex = 4;
            this.label1.Text = "From";
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // button_computeMacPerPeriod
            // 
            this.button_computeMacPerPeriod.Location = new System.Drawing.Point(1829, 479);
            this.button_computeMacPerPeriod.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.button_computeMacPerPeriod.Name = "button_computeMacPerPeriod";
            this.button_computeMacPerPeriod.Size = new System.Drawing.Size(245, 163);
            this.button_computeMacPerPeriod.TabIndex = 3;
            this.button_computeMacPerPeriod.Text = "Compute";
            this.button_computeMacPerPeriod.UseVisualStyleBackColor = true;
            this.button_computeMacPerPeriod.Click += new System.EventHandler(this.Button1_Click_1);
            // 
            // dateTimePicker1_from
            // 
            this.dateTimePicker1_from.Location = new System.Drawing.Point(1748, 184);
            this.dateTimePicker1_from.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.dateTimePicker1_from.Name = "dateTimePicker1_from";
            this.dateTimePicker1_from.Size = new System.Drawing.Size(395, 38);
            this.dateTimePicker1_from.TabIndex = 2;
            this.dateTimePicker1_from.Value = new System.DateTime(2019, 7, 14, 19, 59, 17, 538);
            // 
            // dateTimePicker_to
            // 
            this.dateTimePicker_to.Location = new System.Drawing.Point(1748, 327);
            this.dateTimePicker_to.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.dateTimePicker_to.Name = "dateTimePicker_to";
            this.dateTimePicker_to.Size = new System.Drawing.Size(395, 38);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1049);
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
            this.tabPage_LongTermStatistics.PerformLayout();
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
        private System.Windows.Forms.ListView listView1;
        private Label label3;
        private Label label4;
    }


}