namespace EmcTester_Interface_DemoUI
{
    partial class MainForm
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
            this.button_Devices_PowerOn = new System.Windows.Forms.Button();
            this.button_Devices_PowerOff = new System.Windows.Forms.Button();
            this.button_Devices_Reset = new System.Windows.Forms.Button();
            this.button_Devices_StartTest = new System.Windows.Forms.Button();
            this.groupBox_Devices = new System.Windows.Forms.GroupBox();
            this.comboBox_SendData = new System.Windows.Forms.ComboBox();
            this.button_Send = new System.Windows.Forms.Button();
            this.button_Send_Help = new System.Windows.Forms.Button();
            this.checkBox_Devices_SelectNone = new System.Windows.Forms.CheckBox();
            this.checkBox_Devices_SelectALL = new System.Windows.Forms.CheckBox();
            this.panel_Devices = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_Debug = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button_AllDevices_StartTest = new System.Windows.Forms.Button();
            this.button_AllDevices_Resеt = new System.Windows.Forms.Button();
            this.button_AllDevices_PowerOff = new System.Windows.Forms.Button();
            this.button_AllDevices_PowerOn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_AllDevices_Debug = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_Devices_UpdateInfo_Get = new System.Windows.Forms.Button();
            this.richTextBox_RecvLog = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_Devices_UpdateInfo_Clear = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_PreliminaryTests = new System.Windows.Forms.Button();
            this.button_CAN_EmulatorSendRandom = new System.Windows.Forms.Button();
            this.button_CAN_EmulatorShow = new System.Windows.Forms.Button();
            this.button_CAN_CommunicatorShow = new System.Windows.Forms.Button();
            this.contextMenuStrip_PreliminaryTests = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_PreliminaryTests_ADC = new System.Windows.Forms.ToolStripMenuItem();
            this.showTestADCLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerUI = new System.Windows.Forms.Timer(this.components);
            this.groupBox_Devices.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip_PreliminaryTests.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Devices_PowerOn
            // 
            this.button_Devices_PowerOn.FlatAppearance.BorderSize = 0;
            this.button_Devices_PowerOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Devices_PowerOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Devices_PowerOn.Location = new System.Drawing.Point(13, 6);
            this.button_Devices_PowerOn.Name = "button_Devices_PowerOn";
            this.button_Devices_PowerOn.Size = new System.Drawing.Size(80, 22);
            this.button_Devices_PowerOn.TabIndex = 0;
            this.button_Devices_PowerOn.Text = "Power ON";
            this.button_Devices_PowerOn.UseVisualStyleBackColor = true;
            this.button_Devices_PowerOn.Click += new System.EventHandler(this.button_Devices_PowerOn_Click);
            // 
            // button_Devices_PowerOff
            // 
            this.button_Devices_PowerOff.FlatAppearance.BorderSize = 0;
            this.button_Devices_PowerOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Devices_PowerOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Devices_PowerOff.Location = new System.Drawing.Point(13, 28);
            this.button_Devices_PowerOff.Name = "button_Devices_PowerOff";
            this.button_Devices_PowerOff.Size = new System.Drawing.Size(80, 22);
            this.button_Devices_PowerOff.TabIndex = 1;
            this.button_Devices_PowerOff.Text = "Power OFF";
            this.button_Devices_PowerOff.UseVisualStyleBackColor = true;
            this.button_Devices_PowerOff.Click += new System.EventHandler(this.button_Devices_PowerOff_Click);
            // 
            // button_Devices_Reset
            // 
            this.button_Devices_Reset.FlatAppearance.BorderSize = 0;
            this.button_Devices_Reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Devices_Reset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Devices_Reset.Location = new System.Drawing.Point(13, 50);
            this.button_Devices_Reset.Name = "button_Devices_Reset";
            this.button_Devices_Reset.Size = new System.Drawing.Size(80, 22);
            this.button_Devices_Reset.TabIndex = 2;
            this.button_Devices_Reset.Text = "Restart";
            this.button_Devices_Reset.UseVisualStyleBackColor = true;
            this.button_Devices_Reset.Click += new System.EventHandler(this.button_Devices_Reset_Click);
            // 
            // button_Devices_StartTest
            // 
            this.button_Devices_StartTest.FlatAppearance.BorderSize = 0;
            this.button_Devices_StartTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Devices_StartTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Devices_StartTest.Location = new System.Drawing.Point(13, 72);
            this.button_Devices_StartTest.Name = "button_Devices_StartTest";
            this.button_Devices_StartTest.Size = new System.Drawing.Size(80, 22);
            this.button_Devices_StartTest.TabIndex = 3;
            this.button_Devices_StartTest.Text = "Start test";
            this.button_Devices_StartTest.UseVisualStyleBackColor = true;
            this.button_Devices_StartTest.Click += new System.EventHandler(this.button_Devices_StartTest_Click);
            // 
            // groupBox_Devices
            // 
            this.groupBox_Devices.Controls.Add(this.comboBox_SendData);
            this.groupBox_Devices.Controls.Add(this.button_Send);
            this.groupBox_Devices.Controls.Add(this.button_Send_Help);
            this.groupBox_Devices.Controls.Add(this.checkBox_Devices_SelectNone);
            this.groupBox_Devices.Controls.Add(this.checkBox_Devices_SelectALL);
            this.groupBox_Devices.Controls.Add(this.panel_Devices);
            this.groupBox_Devices.Controls.Add(this.panel1);
            this.groupBox_Devices.Controls.Add(this.label1);
            this.groupBox_Devices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Devices.Location = new System.Drawing.Point(3, 16);
            this.groupBox_Devices.Name = "groupBox_Devices";
            this.groupBox_Devices.Size = new System.Drawing.Size(514, 149);
            this.groupBox_Devices.TabIndex = 4;
            this.groupBox_Devices.TabStop = false;
            this.groupBox_Devices.Text = "Selected devices";
            // 
            // comboBox_SendData
            // 
            this.comboBox_SendData.FormattingEnabled = true;
            this.comboBox_SendData.Location = new System.Drawing.Point(215, 0);
            this.comboBox_SendData.Name = "comboBox_SendData";
            this.comboBox_SendData.Size = new System.Drawing.Size(193, 21);
            this.comboBox_SendData.TabIndex = 8;
            this.comboBox_SendData.SelectedIndexChanged += new System.EventHandler(this.comboBox_SendData_TextChanged);
            this.comboBox_SendData.TextUpdate += new System.EventHandler(this.comboBox_SendData_TextChanged);
            // 
            // button_Send
            // 
            this.button_Send.FlatAppearance.BorderSize = 0;
            this.button_Send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Send.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Send.Location = new System.Drawing.Point(412, 1);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(41, 20);
            this.button_Send.TabIndex = 4;
            this.button_Send.Text = "Send";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.button_Send_Click);
            // 
            // button_Send_Help
            // 
            this.button_Send_Help.FlatAppearance.BorderSize = 0;
            this.button_Send_Help.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Send_Help.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Send_Help.Location = new System.Drawing.Point(457, 1);
            this.button_Send_Help.Name = "button_Send_Help";
            this.button_Send_Help.Size = new System.Drawing.Size(44, 20);
            this.button_Send_Help.TabIndex = 7;
            this.button_Send_Help.Text = "help?";
            this.button_Send_Help.UseVisualStyleBackColor = true;
            this.button_Send_Help.Click += new System.EventHandler(this.button_Send_Help_Click);
            // 
            // checkBox_Devices_SelectNone
            // 
            this.checkBox_Devices_SelectNone.AutoSize = true;
            this.checkBox_Devices_SelectNone.FlatAppearance.BorderSize = 0;
            this.checkBox_Devices_SelectNone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_Devices_SelectNone.Location = new System.Drawing.Point(160, 3);
            this.checkBox_Devices_SelectNone.Name = "checkBox_Devices_SelectNone";
            this.checkBox_Devices_SelectNone.Size = new System.Drawing.Size(47, 17);
            this.checkBox_Devices_SelectNone.TabIndex = 5;
            this.checkBox_Devices_SelectNone.Text = "none";
            this.checkBox_Devices_SelectNone.UseVisualStyleBackColor = true;
            this.checkBox_Devices_SelectNone.CheckedChanged += new System.EventHandler(this.checkBox_Devices_SelectNone_CheckedChanged);
            // 
            // checkBox_Devices_SelectALL
            // 
            this.checkBox_Devices_SelectALL.AutoSize = true;
            this.checkBox_Devices_SelectALL.FlatAppearance.BorderSize = 0;
            this.checkBox_Devices_SelectALL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_Devices_SelectALL.Location = new System.Drawing.Point(111, 3);
            this.checkBox_Devices_SelectALL.Name = "checkBox_Devices_SelectALL";
            this.checkBox_Devices_SelectALL.Size = new System.Drawing.Size(42, 17);
            this.checkBox_Devices_SelectALL.TabIndex = 4;
            this.checkBox_Devices_SelectALL.Text = "ALL";
            this.checkBox_Devices_SelectALL.UseVisualStyleBackColor = true;
            this.checkBox_Devices_SelectALL.CheckedChanged += new System.EventHandler(this.checkBox_Devices_SelectALL_CheckedChanged);
            // 
            // panel_Devices
            // 
            this.panel_Devices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Devices.Location = new System.Drawing.Point(3, 24);
            this.panel_Devices.Name = "panel_Devices";
            this.panel_Devices.Size = new System.Drawing.Size(405, 122);
            this.panel_Devices.TabIndex = 3;
            this.panel_Devices.Resize += new System.EventHandler(this.panel_Devices_Resize);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_Debug);
            this.panel1.Controls.Add(this.button_Devices_PowerOn);
            this.panel1.Controls.Add(this.button_Devices_StartTest);
            this.panel1.Controls.Add(this.button_Devices_PowerOff);
            this.panel1.Controls.Add(this.button_Devices_Reset);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(408, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(103, 122);
            this.panel1.TabIndex = 3;
            // 
            // button_Debug
            // 
            this.button_Debug.FlatAppearance.BorderSize = 0;
            this.button_Debug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Debug.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Debug.Location = new System.Drawing.Point(13, 94);
            this.button_Debug.Name = "button_Debug";
            this.button_Debug.Size = new System.Drawing.Size(80, 22);
            this.button_Debug.TabIndex = 4;
            this.button_Debug.Text = "Debug";
            this.button_Debug.UseVisualStyleBackColor = true;
            this.button_Debug.Click += new System.EventHandler(this.button_Debug_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(508, 8);
            this.label1.TabIndex = 3;
            this.label1.Text = "- - -";
            // 
            // button_AllDevices_StartTest
            // 
            this.button_AllDevices_StartTest.FlatAppearance.BorderSize = 0;
            this.button_AllDevices_StartTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_AllDevices_StartTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_AllDevices_StartTest.Location = new System.Drawing.Point(6, 96);
            this.button_AllDevices_StartTest.Name = "button_AllDevices_StartTest";
            this.button_AllDevices_StartTest.Size = new System.Drawing.Size(128, 22);
            this.button_AllDevices_StartTest.TabIndex = 11;
            this.button_AllDevices_StartTest.Text = "All devices Start test";
            this.button_AllDevices_StartTest.UseVisualStyleBackColor = true;
            this.button_AllDevices_StartTest.Click += new System.EventHandler(this.button_AllDevices_StartTest_Click);
            // 
            // button_AllDevices_Resеt
            // 
            this.button_AllDevices_Resеt.FlatAppearance.BorderSize = 0;
            this.button_AllDevices_Resеt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_AllDevices_Resеt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_AllDevices_Resеt.Location = new System.Drawing.Point(6, 74);
            this.button_AllDevices_Resеt.Name = "button_AllDevices_Resеt";
            this.button_AllDevices_Resеt.Size = new System.Drawing.Size(128, 22);
            this.button_AllDevices_Resеt.TabIndex = 10;
            this.button_AllDevices_Resеt.Text = "All devices Restart";
            this.button_AllDevices_Resеt.UseVisualStyleBackColor = true;
            this.button_AllDevices_Resеt.Click += new System.EventHandler(this.button_AllDevices_Reset_Click);
            // 
            // button_AllDevices_PowerOff
            // 
            this.button_AllDevices_PowerOff.FlatAppearance.BorderSize = 0;
            this.button_AllDevices_PowerOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_AllDevices_PowerOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_AllDevices_PowerOff.Location = new System.Drawing.Point(6, 52);
            this.button_AllDevices_PowerOff.Name = "button_AllDevices_PowerOff";
            this.button_AllDevices_PowerOff.Size = new System.Drawing.Size(128, 22);
            this.button_AllDevices_PowerOff.TabIndex = 9;
            this.button_AllDevices_PowerOff.Text = "All devices Power OFF";
            this.button_AllDevices_PowerOff.UseVisualStyleBackColor = true;
            this.button_AllDevices_PowerOff.Click += new System.EventHandler(this.button_AllDevices_PowerOff_Click);
            // 
            // button_AllDevices_PowerOn
            // 
            this.button_AllDevices_PowerOn.FlatAppearance.BorderSize = 0;
            this.button_AllDevices_PowerOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_AllDevices_PowerOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_AllDevices_PowerOn.Location = new System.Drawing.Point(6, 30);
            this.button_AllDevices_PowerOn.Name = "button_AllDevices_PowerOn";
            this.button_AllDevices_PowerOn.Size = new System.Drawing.Size(128, 22);
            this.button_AllDevices_PowerOn.TabIndex = 8;
            this.button_AllDevices_PowerOn.Text = "All devices Power ON";
            this.button_AllDevices_PowerOn.UseVisualStyleBackColor = true;
            this.button_AllDevices_PowerOn.Click += new System.EventHandler(this.button_AllDevices_PowerOn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_AllDevices_Debug);
            this.groupBox1.Controls.Add(this.button_AllDevices_PowerOn);
            this.groupBox1.Controls.Add(this.button_AllDevices_StartTest);
            this.groupBox1.Controls.Add(this.button_AllDevices_PowerOff);
            this.groupBox1.Controls.Add(this.button_AllDevices_Resеt);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(517, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 149);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ALL devices";
            // 
            // button_AllDevices_Debug
            // 
            this.button_AllDevices_Debug.FlatAppearance.BorderSize = 0;
            this.button_AllDevices_Debug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_AllDevices_Debug.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_AllDevices_Debug.Location = new System.Drawing.Point(6, 118);
            this.button_AllDevices_Debug.Name = "button_AllDevices_Debug";
            this.button_AllDevices_Debug.Size = new System.Drawing.Size(128, 22);
            this.button_AllDevices_Debug.TabIndex = 12;
            this.button_AllDevices_Debug.Text = "All devices Debug";
            this.button_AllDevices_Debug.UseVisualStyleBackColor = true;
            this.button_AllDevices_Debug.Click += new System.EventHandler(this.button_AllDevices_Debug_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(664, 413);
            this.splitContainer1.SplitterDistance = 168;
            this.splitContainer1.TabIndex = 12;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightCyan;
            this.groupBox3.Controls.Add(this.groupBox_Devices);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(664, 168);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Send commands";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Info;
            this.groupBox2.Controls.Add(this.button_Devices_UpdateInfo_Get);
            this.groupBox2.Controls.Add(this.richTextBox_RecvLog);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.button_Devices_UpdateInfo_Clear);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(664, 241);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Update info";
            // 
            // button_Devices_UpdateInfo_Get
            // 
            this.button_Devices_UpdateInfo_Get.FlatAppearance.BorderSize = 0;
            this.button_Devices_UpdateInfo_Get.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Devices_UpdateInfo_Get.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Devices_UpdateInfo_Get.Location = new System.Drawing.Point(108, -4);
            this.button_Devices_UpdateInfo_Get.Name = "button_Devices_UpdateInfo_Get";
            this.button_Devices_UpdateInfo_Get.Size = new System.Drawing.Size(48, 21);
            this.button_Devices_UpdateInfo_Get.TabIndex = 5;
            this.button_Devices_UpdateInfo_Get.Text = "Get";
            this.button_Devices_UpdateInfo_Get.UseVisualStyleBackColor = true;
            this.button_Devices_UpdateInfo_Get.Click += new System.EventHandler(this.button_Devices_UpdateInfo_Get_Click);
            // 
            // richTextBox_RecvLog
            // 
            this.richTextBox_RecvLog.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBox_RecvLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_RecvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_RecvLog.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox_RecvLog.ForeColor = System.Drawing.SystemColors.InfoText;
            this.richTextBox_RecvLog.Location = new System.Drawing.Point(3, 20);
            this.richTextBox_RecvLog.Name = "richTextBox_RecvLog";
            this.richTextBox_RecvLog.ReadOnly = true;
            this.richTextBox_RecvLog.Size = new System.Drawing.Size(658, 218);
            this.richTextBox_RecvLog.TabIndex = 1;
            this.richTextBox_RecvLog.Text = "";
            this.richTextBox_RecvLog.WordWrap = false;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(658, 4);
            this.label2.TabIndex = 4;
            this.label2.Text = "- - -";
            // 
            // button_Devices_UpdateInfo_Clear
            // 
            this.button_Devices_UpdateInfo_Clear.FlatAppearance.BorderSize = 0;
            this.button_Devices_UpdateInfo_Clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Devices_UpdateInfo_Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Devices_UpdateInfo_Clear.Location = new System.Drawing.Point(162, -4);
            this.button_Devices_UpdateInfo_Clear.Name = "button_Devices_UpdateInfo_Clear";
            this.button_Devices_UpdateInfo_Clear.Size = new System.Drawing.Size(48, 21);
            this.button_Devices_UpdateInfo_Clear.TabIndex = 2;
            this.button_Devices_UpdateInfo_Clear.Text = "Clear";
            this.button_Devices_UpdateInfo_Clear.UseVisualStyleBackColor = true;
            this.button_Devices_UpdateInfo_Clear.Click += new System.EventHandler(this.button_Devices_UpdateInfo_Clear_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_PreliminaryTests);
            this.panel2.Controls.Add(this.button_CAN_EmulatorSendRandom);
            this.panel2.Controls.Add(this.button_CAN_EmulatorShow);
            this.panel2.Controls.Add(this.button_CAN_CommunicatorShow);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(664, 28);
            this.panel2.TabIndex = 13;
            // 
            // button_PreliminaryTests
            // 
            this.button_PreliminaryTests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_PreliminaryTests.Location = new System.Drawing.Point(12, 0);
            this.button_PreliminaryTests.Name = "button_PreliminaryTests";
            this.button_PreliminaryTests.Size = new System.Drawing.Size(104, 28);
            this.button_PreliminaryTests.TabIndex = 4;
            this.button_PreliminaryTests.Text = "Preliminary tests";
            this.button_PreliminaryTests.UseVisualStyleBackColor = true;
            this.button_PreliminaryTests.Click += new System.EventHandler(this.button_PreliminaryTests_Click);
            // 
            // button_CAN_EmulatorSendRandom
            // 
            this.button_CAN_EmulatorSendRandom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_CAN_EmulatorSendRandom.Location = new System.Drawing.Point(167, 0);
            this.button_CAN_EmulatorSendRandom.Name = "button_CAN_EmulatorSendRandom";
            this.button_CAN_EmulatorSendRandom.Size = new System.Drawing.Size(104, 28);
            this.button_CAN_EmulatorSendRandom.TabIndex = 3;
            this.button_CAN_EmulatorSendRandom.Text = "Send random emu";
            this.button_CAN_EmulatorSendRandom.UseVisualStyleBackColor = true;
            this.button_CAN_EmulatorSendRandom.Click += new System.EventHandler(this.button_CAN_EmulatorSendRandom_Click);
            // 
            // button_CAN_EmulatorShow
            // 
            this.button_CAN_EmulatorShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_CAN_EmulatorShow.Location = new System.Drawing.Point(276, 0);
            this.button_CAN_EmulatorShow.Name = "button_CAN_EmulatorShow";
            this.button_CAN_EmulatorShow.Size = new System.Drawing.Size(104, 28);
            this.button_CAN_EmulatorShow.TabIndex = 2;
            this.button_CAN_EmulatorShow.Text = "Emu CAN terminal";
            this.button_CAN_EmulatorShow.UseVisualStyleBackColor = true;
            this.button_CAN_EmulatorShow.Click += new System.EventHandler(this.button_CAN_EmulatorShow_Click);
            // 
            // button_CAN_CommunicatorShow
            // 
            this.button_CAN_CommunicatorShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_CAN_CommunicatorShow.Location = new System.Drawing.Point(396, 0);
            this.button_CAN_CommunicatorShow.Name = "button_CAN_CommunicatorShow";
            this.button_CAN_CommunicatorShow.Size = new System.Drawing.Size(259, 28);
            this.button_CAN_CommunicatorShow.TabIndex = 1;
            this.button_CAN_CommunicatorShow.Text = "Show Communicator CAN terminal";
            this.button_CAN_CommunicatorShow.UseVisualStyleBackColor = true;
            this.button_CAN_CommunicatorShow.Click += new System.EventHandler(this.button_CAN_CommunicatorShow_Click);
            // 
            // contextMenuStrip_PreliminaryTests
            // 
            this.contextMenuStrip_PreliminaryTests.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_PreliminaryTests_ADC,
            this.showTestADCLogToolStripMenuItem});
            this.contextMenuStrip_PreliminaryTests.Name = "contextMenuStrip_PreliminaryTests";
            this.contextMenuStrip_PreliminaryTests.Size = new System.Drawing.Size(177, 48);
            // 
            // toolStripMenuItem_PreliminaryTests_ADC
            // 
            this.toolStripMenuItem_PreliminaryTests_ADC.Name = "toolStripMenuItem_PreliminaryTests_ADC";
            this.toolStripMenuItem_PreliminaryTests_ADC.Size = new System.Drawing.Size(176, 22);
            this.toolStripMenuItem_PreliminaryTests_ADC.Text = "Show test ADC grid";
            this.toolStripMenuItem_PreliminaryTests_ADC.Click += new System.EventHandler(this.toolStripMenuItem_PreliminaryTests_ADC_Click);
            // 
            // showTestADCLogToolStripMenuItem
            // 
            this.showTestADCLogToolStripMenuItem.Name = "showTestADCLogToolStripMenuItem";
            this.showTestADCLogToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.showTestADCLogToolStripMenuItem.Text = "Show test ADC log";
            this.showTestADCLogToolStripMenuItem.Click += new System.EventHandler(this.showTestADCLogToolStripMenuItem_Click);
            // 
            // timerUI
            // 
            this.timerUI.Tick += new System.EventHandler(this.timerUI_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 441);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.MinimumSize = new System.Drawing.Size(600, 480);
            this.Name = "MainForm";
            this.Text = "EmcTester Interface Demo UI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.groupBox_Devices.ResumeLayout(false);
            this.groupBox_Devices.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.contextMenuStrip_PreliminaryTests.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Devices_PowerOn;
        private System.Windows.Forms.Button button_Devices_PowerOff;
        private System.Windows.Forms.Button button_Devices_Reset;
        private System.Windows.Forms.Button button_Devices_StartTest;
        private System.Windows.Forms.GroupBox groupBox_Devices;
        private System.Windows.Forms.Panel panel_Devices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_AllDevices_StartTest;
        private System.Windows.Forms.Button button_AllDevices_Resеt;
        private System.Windows.Forms.Button button_AllDevices_PowerOff;
        private System.Windows.Forms.Button button_AllDevices_PowerOn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_CAN_CommunicatorShow;
        private System.Windows.Forms.CheckBox checkBox_Devices_SelectNone;
        private System.Windows.Forms.CheckBox checkBox_Devices_SelectALL;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox richTextBox_RecvLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_Devices_UpdateInfo_Clear;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_Devices_UpdateInfo_Get;
        private System.Windows.Forms.Button button_CAN_EmulatorShow;
        private System.Windows.Forms.Button button_CAN_EmulatorSendRandom;
        private System.Windows.Forms.Button button_PreliminaryTests;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_PreliminaryTests;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_PreliminaryTests_ADC;
        private System.Windows.Forms.ToolStripMenuItem showTestADCLogToolStripMenuItem;
        private System.Windows.Forms.Timer timerUI;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.Button button_Send_Help;
        private System.Windows.Forms.Button button_Debug;
        private System.Windows.Forms.Button button_AllDevices_Debug;
        private System.Windows.Forms.ComboBox comboBox_SendData;
    }
}

