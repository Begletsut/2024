namespace EMC20_emuLambdaControl
{
    partial class Form_ChartViewer
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
            this.chart_1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.numericUpDown_TimerFrame = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_Run = new System.Windows.Forms.CheckBox();
            this.panel_Top = new System.Windows.Forms.Panel();
            this.panel_SerialPort = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_EmuRPM = new System.Windows.Forms.NumericUpDown();
            this.checkBox_Emu = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_EmuOxy = new System.Windows.Forms.NumericUpDown();
            this.panel_Controls = new System.Windows.Forms.Panel();
            this.label_PackCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TimerFrame)).BeginInit();
            this.panel_Top.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_EmuRPM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_EmuOxy)).BeginInit();
            this.panel_Controls.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart_1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_1.ChartAreas.Add(chartArea1);
            this.chart_1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart_1.Legends.Add(legend1);
            this.chart_1.Location = new System.Drawing.Point(0, 86);
            this.chart_1.Name = "chart_1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_1.Series.Add(series1);
            this.chart_1.Size = new System.Drawing.Size(1141, 602);
            this.chart_1.TabIndex = 0;
            this.chart_1.Text = "chart1";
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // numericUpDown_TimerFrame
            // 
            this.numericUpDown_TimerFrame.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown_TimerFrame.Location = new System.Drawing.Point(15, 48);
            this.numericUpDown_TimerFrame.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDown_TimerFrame.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_TimerFrame.Name = "numericUpDown_TimerFrame";
            this.numericUpDown_TimerFrame.Size = new System.Drawing.Size(68, 20);
            this.numericUpDown_TimerFrame.TabIndex = 1;
            this.numericUpDown_TimerFrame.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_TimerFrame.ValueChanged += new System.EventHandler(this.numericUpDown_TimerFrame_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Time frame [ms] :";
            // 
            // checkBox_Run
            // 
            this.checkBox_Run.AutoSize = true;
            this.checkBox_Run.Location = new System.Drawing.Point(15, 12);
            this.checkBox_Run.Name = "checkBox_Run";
            this.checkBox_Run.Size = new System.Drawing.Size(46, 17);
            this.checkBox_Run.TabIndex = 6;
            this.checkBox_Run.Text = "Run";
            this.checkBox_Run.UseVisualStyleBackColor = true;
            // 
            // panel_Top
            // 
            this.panel_Top.Controls.Add(this.panel_SerialPort);
            this.panel_Top.Controls.Add(this.panel1);
            this.panel_Top.Controls.Add(this.panel_Controls);
            this.panel_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Top.Location = new System.Drawing.Point(0, 0);
            this.panel_Top.Name = "panel_Top";
            this.panel_Top.Size = new System.Drawing.Size(1141, 86);
            this.panel_Top.TabIndex = 7;
            // 
            // panel_SerialPort
            // 
            this.panel_SerialPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_SerialPort.Location = new System.Drawing.Point(0, 0);
            this.panel_SerialPort.Name = "panel_SerialPort";
            this.panel_SerialPort.Size = new System.Drawing.Size(799, 86);
            this.panel_SerialPort.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.numericUpDown_EmuRPM);
            this.panel1.Controls.Add(this.checkBox_Emu);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.numericUpDown_EmuOxy);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(799, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(197, 86);
            this.panel1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "RPM:   0 .. 4000";
            // 
            // numericUpDown_EmuRPM
            // 
            this.numericUpDown_EmuRPM.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown_EmuRPM.Location = new System.Drawing.Point(97, 56);
            this.numericUpDown_EmuRPM.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.numericUpDown_EmuRPM.Name = "numericUpDown_EmuRPM";
            this.numericUpDown_EmuRPM.Size = new System.Drawing.Size(68, 20);
            this.numericUpDown_EmuRPM.TabIndex = 7;
            this.numericUpDown_EmuRPM.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // checkBox_Emu
            // 
            this.checkBox_Emu.AutoSize = true;
            this.checkBox_Emu.Location = new System.Drawing.Point(118, 12);
            this.checkBox_Emu.Name = "checkBox_Emu";
            this.checkBox_Emu.Size = new System.Drawing.Size(47, 17);
            this.checkBox_Emu.TabIndex = 6;
            this.checkBox_Emu.Text = "Emu";
            this.checkBox_Emu.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Oxy:   0 .. 4000";
            // 
            // numericUpDown_EmuOxy
            // 
            this.numericUpDown_EmuOxy.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown_EmuOxy.Location = new System.Drawing.Point(97, 35);
            this.numericUpDown_EmuOxy.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.numericUpDown_EmuOxy.Name = "numericUpDown_EmuOxy";
            this.numericUpDown_EmuOxy.Size = new System.Drawing.Size(68, 20);
            this.numericUpDown_EmuOxy.TabIndex = 1;
            this.numericUpDown_EmuOxy.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // panel_Controls
            // 
            this.panel_Controls.Controls.Add(this.label_PackCount);
            this.panel_Controls.Controls.Add(this.checkBox_Run);
            this.panel_Controls.Controls.Add(this.label2);
            this.panel_Controls.Controls.Add(this.numericUpDown_TimerFrame);
            this.panel_Controls.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_Controls.Location = new System.Drawing.Point(996, 0);
            this.panel_Controls.Name = "panel_Controls";
            this.panel_Controls.Size = new System.Drawing.Size(145, 86);
            this.panel_Controls.TabIndex = 1;
            // 
            // label_PackCount
            // 
            this.label_PackCount.AutoSize = true;
            this.label_PackCount.Location = new System.Drawing.Point(67, 13);
            this.label_PackCount.Name = "label_PackCount";
            this.label_PackCount.Size = new System.Drawing.Size(35, 13);
            this.label_PackCount.TabIndex = 7;
            this.label_PackCount.Text = "label4";
            // 
            // Form_ChartViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1141, 688);
            this.Controls.Add(this.chart_1);
            this.Controls.Add(this.panel_Top);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_ChartViewer";
            this.Text = "TI-540 three phase currents log viewer";
            this.Load += new System.EventHandler(this.Form_ISF_ChartViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TimerFrame)).EndInit();
            this.panel_Top.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_EmuRPM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_EmuOxy)).EndInit();
            this.panel_Controls.ResumeLayout(false);
            this.panel_Controls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown numericUpDown_TimerFrame;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_Run;
        private System.Windows.Forms.Panel panel_Top;
        private System.Windows.Forms.Panel panel_SerialPort;
        private System.Windows.Forms.Panel panel_Controls;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox_Emu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_EmuOxy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown_EmuRPM;
        private System.Windows.Forms.Label label_PackCount;
    }
}