namespace EmcTester_DirectLIN
{
    partial class Form_DirectLIN
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel_Top = new System.Windows.Forms.Panel();
            this.panel_SerialPort = new System.Windows.Forms.Panel();
            this.button_SerialPort_NewLogFile = new System.Windows.Forms.Button();
            this.button_ShowDataView = new System.Windows.Forms.Button();
            this.panel_Series = new System.Windows.Forms.Panel();
            this.label_TestLoop = new System.Windows.Forms.Label();
            this.checkBox_TestLoop = new System.Windows.Forms.CheckBox();
            this.TextBox_ToSend = new System.Windows.Forms.TextBox();
            this.button_SendStr = new System.Windows.Forms.Button();
            this.button_SetEmcToTestMode = new System.Windows.Forms.Button();
            this.richTextBox_Log = new System.Windows.Forms.RichTextBox();
            this.panel_Top.SuspendLayout();
            this.panel_Series.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel_Top
            // 
            this.panel_Top.Controls.Add(this.panel_SerialPort);
            this.panel_Top.Controls.Add(this.panel_Series);
            this.panel_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Top.Location = new System.Drawing.Point(0, 0);
            this.panel_Top.Name = "panel_Top";
            this.panel_Top.Size = new System.Drawing.Size(784, 88);
            this.panel_Top.TabIndex = 7;
            // 
            // panel_SerialPort
            // 
            this.panel_SerialPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_SerialPort.Location = new System.Drawing.Point(0, 0);
            this.panel_SerialPort.Name = "panel_SerialPort";
            this.panel_SerialPort.Size = new System.Drawing.Size(476, 88);
            this.panel_SerialPort.TabIndex = 0;
            // 
            // button_SerialPort_NewLogFile
            // 
            this.button_SerialPort_NewLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SerialPort_NewLogFile.Location = new System.Drawing.Point(81, 10);
            this.button_SerialPort_NewLogFile.Name = "button_SerialPort_NewLogFile";
            this.button_SerialPort_NewLogFile.Size = new System.Drawing.Size(109, 20);
            this.button_SerialPort_NewLogFile.TabIndex = 15;
            this.button_SerialPort_NewLogFile.Text = "New Log file";
            this.button_SerialPort_NewLogFile.UseVisualStyleBackColor = true;
            // 
            // button_ShowDataView
            // 
            this.button_ShowDataView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ShowDataView.Location = new System.Drawing.Point(196, 10);
            this.button_ShowDataView.Name = "button_ShowDataView";
            this.button_ShowDataView.Size = new System.Drawing.Size(109, 20);
            this.button_ShowDataView.TabIndex = 14;
            this.button_ShowDataView.Text = "Show data view";
            this.button_ShowDataView.UseVisualStyleBackColor = true;
            // 
            // panel_Series
            // 
            this.panel_Series.Controls.Add(this.button_ShowDataView);
            this.panel_Series.Controls.Add(this.button_SerialPort_NewLogFile);
            this.panel_Series.Controls.Add(this.label_TestLoop);
            this.panel_Series.Controls.Add(this.checkBox_TestLoop);
            this.panel_Series.Controls.Add(this.TextBox_ToSend);
            this.panel_Series.Controls.Add(this.button_SendStr);
            this.panel_Series.Controls.Add(this.button_SetEmcToTestMode);
            this.panel_Series.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_Series.Location = new System.Drawing.Point(476, 0);
            this.panel_Series.Name = "panel_Series";
            this.panel_Series.Size = new System.Drawing.Size(308, 88);
            this.panel_Series.TabIndex = 3;
            // 
            // label_TestLoop
            // 
            this.label_TestLoop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_TestLoop.AutoSize = true;
            this.label_TestLoop.Location = new System.Drawing.Point(60, 40);
            this.label_TestLoop.Name = "label_TestLoop";
            this.label_TestLoop.Size = new System.Drawing.Size(22, 13);
            this.label_TestLoop.TabIndex = 20;
            this.label_TestLoop.Text = "- - -";
            // 
            // checkBox_TestLoop
            // 
            this.checkBox_TestLoop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_TestLoop.AutoSize = true;
            this.checkBox_TestLoop.Location = new System.Drawing.Point(120, 39);
            this.checkBox_TestLoop.Name = "checkBox_TestLoop";
            this.checkBox_TestLoop.Size = new System.Drawing.Size(70, 17);
            this.checkBox_TestLoop.TabIndex = 19;
            this.checkBox_TestLoop.Text = "Test loop";
            this.checkBox_TestLoop.UseVisualStyleBackColor = true;
            this.checkBox_TestLoop.CheckedChanged += new System.EventHandler(this.checkBox_TestLoop_CheckedChanged);
            // 
            // TextBox_ToSend
            // 
            this.TextBox_ToSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_ToSend.Location = new System.Drawing.Point(9, 62);
            this.TextBox_ToSend.Name = "TextBox_ToSend";
            this.TextBox_ToSend.Size = new System.Drawing.Size(216, 20);
            this.TextBox_ToSend.TabIndex = 18;
            // 
            // button_SendStr
            // 
            this.button_SendStr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SendStr.Location = new System.Drawing.Point(228, 62);
            this.button_SendStr.Name = "button_SendStr";
            this.button_SendStr.Size = new System.Drawing.Size(77, 20);
            this.button_SendStr.TabIndex = 17;
            this.button_SendStr.Text = "Send \\r\\n";
            this.button_SendStr.UseVisualStyleBackColor = true;
            this.button_SendStr.Click += new System.EventHandler(this.button_SendStr_Click);
            // 
            // button_SetEmcToTestMode
            // 
            this.button_SetEmcToTestMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SetEmcToTestMode.Location = new System.Drawing.Point(196, 36);
            this.button_SetEmcToTestMode.Name = "button_SetEmcToTestMode";
            this.button_SetEmcToTestMode.Size = new System.Drawing.Size(109, 20);
            this.button_SetEmcToTestMode.TabIndex = 16;
            this.button_SetEmcToTestMode.Text = "Set EMC to test mode";
            this.button_SetEmcToTestMode.UseVisualStyleBackColor = true;
            this.button_SetEmcToTestMode.Click += new System.EventHandler(this.button_SetEmcToTestMode_Click);
            // 
            // richTextBox_Log
            // 
            this.richTextBox_Log.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBox_Log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Log.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox_Log.ForeColor = System.Drawing.SystemColors.InfoText;
            this.richTextBox_Log.Location = new System.Drawing.Point(0, 88);
            this.richTextBox_Log.Name = "richTextBox_Log";
            this.richTextBox_Log.Size = new System.Drawing.Size(784, 473);
            this.richTextBox_Log.TabIndex = 17;
            this.richTextBox_Log.Text = "";
            // 
            // Form_DirectLIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.richTextBox_Log);
            this.Controls.Add(this.panel_Top);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "Form_DirectLIN";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_ChartViewer_FormClosing);
            this.panel_Top.ResumeLayout(false);
            this.panel_Series.ResumeLayout(false);
            this.panel_Series.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel_Top;
        private System.Windows.Forms.Panel panel_SerialPort;
        private System.Windows.Forms.Button button_SerialPort_NewLogFile;
        private System.Windows.Forms.Panel panel_Series;
        private System.Windows.Forms.Button button_ShowDataView;
        private System.Windows.Forms.Button button_SetEmcToTestMode;
        private System.Windows.Forms.RichTextBox richTextBox_Log;
        private System.Windows.Forms.TextBox TextBox_ToSend;
        private System.Windows.Forms.Button button_SendStr;
        private System.Windows.Forms.Label label_TestLoop;
        private System.Windows.Forms.CheckBox checkBox_TestLoop;
    }
}