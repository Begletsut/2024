namespace isf_SerialPort_Config
{
    public partial class ucSerialPort_Config
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelFill = new System.Windows.Forms.Panel();
            this.richTextBox_Info = new System.Windows.Forms.RichTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkBox_LogOn = new System.Windows.Forms.CheckBox();
            this.button_ShowLog = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox_SerialPort = new System.Windows.Forms.ComboBox();
            this.button_SerialPortConfig = new System.Windows.Forms.Button();
            this.label_cSerialPort = new System.Windows.Forms.Label();
            this.contextMenuStrip_SerialPortConfig = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripComboBox_BdRate = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBox_DataBits = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBox_Parity = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBox_StopBits = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBox_Handshake = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripTextBox_ReadTimeout_ms = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox_WriteTimeout_ms = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem_SerialPortConfig_OK = new System.Windows.Forms.ToolStripMenuItem();
            this.cANCELToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelFill.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip_SerialPortConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFill
            // 
            this.panelFill.Controls.Add(this.richTextBox_Info);
            this.panelFill.Controls.Add(this.panel4);
            this.panelFill.Controls.Add(this.panel1);
            this.panelFill.Controls.Add(this.label_cSerialPort);
            this.panelFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFill.Location = new System.Drawing.Point(0, 0);
            this.panelFill.Name = "panelFill";
            this.panelFill.Size = new System.Drawing.Size(444, 44);
            this.panelFill.TabIndex = 1;
            // 
            // richTextBox_Info
            // 
            this.richTextBox_Info.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBox_Info.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Info.ForeColor = System.Drawing.SystemColors.InfoText;
            this.richTextBox_Info.Location = new System.Drawing.Point(164, 0);
            this.richTextBox_Info.Name = "richTextBox_Info";
            this.richTextBox_Info.ReadOnly = true;
            this.richTextBox_Info.Size = new System.Drawing.Size(280, 44);
            this.richTextBox_Info.TabIndex = 1;
            this.richTextBox_Info.Text = "";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.checkBox_LogOn);
            this.panel4.Controls.Add(this.button_ShowLog);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(100, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(64, 44);
            this.panel4.TabIndex = 12;
            // 
            // checkBox_LogOn
            // 
            this.checkBox_LogOn.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkBox_LogOn.FlatAppearance.BorderSize = 0;
            this.checkBox_LogOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_LogOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_LogOn.Location = new System.Drawing.Point(8, 21);
            this.checkBox_LogOn.Name = "checkBox_LogOn";
            this.checkBox_LogOn.Size = new System.Drawing.Size(56, 23);
            this.checkBox_LogOn.TabIndex = 1;
            this.checkBox_LogOn.Text = "LogOn";
            this.checkBox_LogOn.UseVisualStyleBackColor = true;
            // 
            // button_ShowLog
            // 
            this.button_ShowLog.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_ShowLog.FlatAppearance.BorderSize = 0;
            this.button_ShowLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ShowLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_ShowLog.Location = new System.Drawing.Point(0, 0);
            this.button_ShowLog.Name = "button_ShowLog";
            this.button_ShowLog.Size = new System.Drawing.Size(64, 21);
            this.button_ShowLog.TabIndex = 0;
            this.button_ShowLog.Text = "Show Log";
            this.button_ShowLog.UseVisualStyleBackColor = true;
            this.button_ShowLog.Click += new System.EventHandler(this.button_ShowLog_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox_SerialPort);
            this.panel1.Controls.Add(this.button_SerialPortConfig);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(36, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(64, 44);
            this.panel1.TabIndex = 9;
            // 
            // comboBox_SerialPort
            // 
            this.comboBox_SerialPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_SerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SerialPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_SerialPort.FormattingEnabled = true;
            this.comboBox_SerialPort.Location = new System.Drawing.Point(0, 21);
            this.comboBox_SerialPort.Name = "comboBox_SerialPort";
            this.comboBox_SerialPort.Size = new System.Drawing.Size(64, 21);
            this.comboBox_SerialPort.TabIndex = 1;
            this.comboBox_SerialPort.DropDown += new System.EventHandler(this.comboBox_SerialPort_DropDown);
            this.comboBox_SerialPort.SelectedIndexChanged += new System.EventHandler(this.comboBox_SerialPort_SelectedIndexChanged);
            // 
            // button_SerialPortConfig
            // 
            this.button_SerialPortConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_SerialPortConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_SerialPortConfig.FlatAppearance.BorderSize = 0;
            this.button_SerialPortConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_SerialPortConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_SerialPortConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_SerialPortConfig.Location = new System.Drawing.Point(0, 0);
            this.button_SerialPortConfig.Name = "button_SerialPortConfig";
            this.button_SerialPortConfig.Size = new System.Drawing.Size(64, 21);
            this.button_SerialPortConfig.TabIndex = 0;
            this.button_SerialPortConfig.Text = "Config";
            this.button_SerialPortConfig.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_SerialPortConfig.UseVisualStyleBackColor = true;
            this.button_SerialPortConfig.Click += new System.EventHandler(this.button_SerialPort_Click);
            // 
            // label_cSerialPort
            // 
            this.label_cSerialPort.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_cSerialPort.Location = new System.Drawing.Point(0, 0);
            this.label_cSerialPort.Name = "label_cSerialPort";
            this.label_cSerialPort.Size = new System.Drawing.Size(36, 44);
            this.label_cSerialPort.TabIndex = 0;
            this.label_cSerialPort.Text = "Serial\r\nport";
            this.label_cSerialPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // contextMenuStrip_SerialPortConfig
            // 
            this.contextMenuStrip_SerialPortConfig.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.toolStripComboBox_BdRate,
            this.toolStripComboBox_DataBits,
            this.toolStripComboBox_Parity,
            this.toolStripComboBox_StopBits,
            this.toolStripComboBox_Handshake,
            this.toolStripTextBox_ReadTimeout_ms,
            this.toolStripTextBox_WriteTimeout_ms,
            this.toolStripMenuItem_SerialPortConfig_OK,
            this.cANCELToolStripMenuItem});
            this.contextMenuStrip_SerialPortConfig.Name = "contextMenuStrip_SerialPortConfig";
            this.contextMenuStrip_SerialPortConfig.ShowImageMargin = false;
            this.contextMenuStrip_SerialPortConfig.Size = new System.Drawing.Size(157, 251);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.toolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.ReadOnly = true;
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 16);
            this.toolStripTextBox1.Text = "Serial port config";
            this.toolStripTextBox1.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // toolStripComboBox_BdRate
            // 
            this.toolStripComboBox_BdRate.BackColor = System.Drawing.SystemColors.Window;
            this.toolStripComboBox_BdRate.Name = "toolStripComboBox_BdRate";
            this.toolStripComboBox_BdRate.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox_BdRate.Text = "baud rate";
            this.toolStripComboBox_BdRate.ToolTipText = "Baud rate";
            // 
            // toolStripComboBox_DataBits
            // 
            this.toolStripComboBox_DataBits.Name = "toolStripComboBox_DataBits";
            this.toolStripComboBox_DataBits.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox_DataBits.Text = "data bits";
            this.toolStripComboBox_DataBits.ToolTipText = "Data bits";
            // 
            // toolStripComboBox_Parity
            // 
            this.toolStripComboBox_Parity.Name = "toolStripComboBox_Parity";
            this.toolStripComboBox_Parity.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox_Parity.Text = "parity";
            this.toolStripComboBox_Parity.ToolTipText = "Parity";
            // 
            // toolStripComboBox_StopBits
            // 
            this.toolStripComboBox_StopBits.Name = "toolStripComboBox_StopBits";
            this.toolStripComboBox_StopBits.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox_StopBits.Text = "stop bits";
            this.toolStripComboBox_StopBits.ToolTipText = "Stop bits";
            // 
            // toolStripComboBox_Handshake
            // 
            this.toolStripComboBox_Handshake.Name = "toolStripComboBox_Handshake";
            this.toolStripComboBox_Handshake.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox_Handshake.Text = "handshake";
            this.toolStripComboBox_Handshake.ToolTipText = "Handshake";
            // 
            // toolStripTextBox_ReadTimeout_ms
            // 
            this.toolStripTextBox_ReadTimeout_ms.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox_ReadTimeout_ms.Name = "toolStripTextBox_ReadTimeout_ms";
            this.toolStripTextBox_ReadTimeout_ms.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox_ReadTimeout_ms.ToolTipText = "Read тimeout ms (-1: none)";
            // 
            // toolStripTextBox_WriteTimeout_ms
            // 
            this.toolStripTextBox_WriteTimeout_ms.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox_WriteTimeout_ms.Name = "toolStripTextBox_WriteTimeout_ms";
            this.toolStripTextBox_WriteTimeout_ms.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox_WriteTimeout_ms.ToolTipText = "Write timeout ms (-1: none)";
            // 
            // toolStripMenuItem_SerialPortConfig_OK
            // 
            this.toolStripMenuItem_SerialPortConfig_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            this.toolStripMenuItem_SerialPortConfig_OK.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem_SerialPortConfig_OK.Name = "toolStripMenuItem_SerialPortConfig_OK";
            this.toolStripMenuItem_SerialPortConfig_OK.Size = new System.Drawing.Size(156, 22);
            this.toolStripMenuItem_SerialPortConfig_OK.Text = "      O K";
            this.toolStripMenuItem_SerialPortConfig_OK.Click += new System.EventHandler(this.toolStripMenuItem_SerialPortConfig_OK_Click);
            // 
            // cANCELToolStripMenuItem
            // 
            this.cANCELToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.cANCELToolStripMenuItem.Name = "cANCELToolStripMenuItem";
            this.cANCELToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.cANCELToolStripMenuItem.Text = "   Cancel";
            // 
            // ucSerialPort_Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panelFill);
            this.MinimumSize = new System.Drawing.Size(240, 44);
            this.Name = "ucSerialPort_Config";
            this.Size = new System.Drawing.Size(444, 44);
            this.panelFill.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.contextMenuStrip_SerialPortConfig.ResumeLayout(false);
            this.contextMenuStrip_SerialPortConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFill;
        private System.Windows.Forms.RichTextBox richTextBox_Info;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button_ShowLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox_SerialPort;
        private System.Windows.Forms.Button button_SerialPortConfig;
        private System.Windows.Forms.Label label_cSerialPort;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_SerialPortConfig;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_BdRate;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_DataBits;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_Parity;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_StopBits;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_Handshake;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_ReadTimeout_ms;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_WriteTimeout_ms;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_SerialPortConfig_OK;
        private System.Windows.Forms.ToolStripMenuItem cANCELToolStripMenuItem;
        public System.Windows.Forms.CheckBox checkBox_LogOn;
    }
}
