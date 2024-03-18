using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

using isf_core;

namespace isf_SerialPort_Config
{
    /// <summary>
    /// simple, common and complete GUI to configure of a physical serial port
    /// </summary>
    public partial class ucSerialPort_Config : UserControl
    {
        public object CommTag = null;

        private const UInt32 DEFAULT_BaudRate = 115200;
        private const byte DEFAULT_DataBits = 8;
        private const Parity DEFAULT_Parity = Parity.None;
        private const StopBits DEFAULT_StopBits = StopBits.One;
        private const Handshake DEFAULT_Handshake = Handshake.None;
        private const UInt32 DEFAULT_ReadTimeout_ms = 2000;

        private const UInt32 DEFAULT_WriteTimeout_ms = 2000;

        private static byte SerialPortCount = 0;
        public SerialPort SerialPort { get; private set; }

        private const string MENU_ITEM_CLOSE = "Close";
        private EventHandler EvHandler;

        private OnLogString LogString;
        private ToolTip ToolTipSerialPortConfig;

        public string Descriptopn { get; private set; }
        public bool LogEnabled { get { return checkBox_LogOn.Checked; } }

        public ucSerialPort_Config() : this("empty", null)
        {
        }

        public ucSerialPort_Config(string aTitle, SerialPort aSerialPort)
        {
            InitializeComponent();

            ToolTipSerialPortConfig = new ToolTip();
            comboBox_SerialPort.Text = MENU_ITEM_CLOSE;
            Initialize_SerialPortConfig();

            SetSerialPort(aSerialPort, false);
            comboBox_SerialPort_SelectedIndexChanged(null, null);
        }

        public void Init(object aCommTag, OnLogString aLogString, EventHandler aEvHandler, Color aBackColor, string aDescriptopn, bool aLogEnabled)
        {
            CommTag = aCommTag;
            LogString = aLogString;
            EvHandler = aEvHandler;
            Descriptopn = aDescriptopn;

            checkBox_LogOn.Checked = aLogEnabled;

            button_ShowLog.Visible = EvHandler != null;
            panel_EmuGUI_ComPort_Resize(null, null);
            
            BackColor = aBackColor;
            label_cSerialPort.BackColor = aBackColor;
            button_SerialPortConfig.BackColor = aBackColor;
            button_ShowLog.BackColor = aBackColor;
            checkBox_LogOn.BackColor = aBackColor;
            richTextBox_Info.BackColor = aBackColor;
        }

        private void Initialize_SerialPortConfig()
        {
            toolStripComboBox_BdRate.DropDownStyle = ComboBoxStyle.DropDown;
            toolStripComboBox_BdRate.Items.Clear();
            toolStripComboBox_BdRate.Items.Add(300);
            toolStripComboBox_BdRate.Items.Add(600);
            toolStripComboBox_BdRate.Items.Add(1200);
            toolStripComboBox_BdRate.Items.Add(2400);
            toolStripComboBox_BdRate.Items.Add(4800);
            toolStripComboBox_BdRate.Items.Add(9600);
            toolStripComboBox_BdRate.Items.Add(10400);
            toolStripComboBox_BdRate.Items.Add(14400);
            toolStripComboBox_BdRate.Items.Add(19200);
            toolStripComboBox_BdRate.Items.Add(28800);
            toolStripComboBox_BdRate.Items.Add(31250);
            toolStripComboBox_BdRate.Items.Add(38400);
            toolStripComboBox_BdRate.Items.Add(56000);
            toolStripComboBox_BdRate.Items.Add(57600);
            toolStripComboBox_BdRate.Items.Add(74880);
            toolStripComboBox_BdRate.Items.Add(115200);
            toolStripComboBox_BdRate.Items.Add(230400);
            toolStripComboBox_BdRate.Items.Add(250000);
            toolStripComboBox_BdRate.Items.Add(256000);
            toolStripComboBox_BdRate.Items.Add(460800);
            toolStripComboBox_BdRate.Items.Add(500000);
            toolStripComboBox_BdRate.Items.Add(512000);
            toolStripComboBox_BdRate.Items.Add(921600);
            toolStripComboBox_BdRate.Items.Add(1000000);
            toolStripComboBox_BdRate.Items.Add(1152000);
            toolStripComboBox_BdRate.Items.Add(1200000);
            toolStripComboBox_BdRate.Items.Add(1389240);
            toolStripComboBox_BdRate.Items.Add(1500000);
            toolStripComboBox_BdRate.Items.Add(1800000);
            toolStripComboBox_BdRate.Items.Add(2000000);
            toolStripComboBox_BdRate.Items.Add(3000000);
            toolStripComboBox_BdRate.Items.Add(4000000);
            toolStripComboBox_BdRate.Items.Add(6000000);
            toolStripComboBox_BdRate.Items.Add(8000000);
            toolStripComboBox_BdRate.Items.Add(10000000);
            toolStripComboBox_BdRate.Items.Add(12000000);
            toolStripComboBox_BdRate.Items.Add(16000000);
            toolStripComboBox_BdRate.Text = DEFAULT_BaudRate.ToString();

            toolStripComboBox_DataBits.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboBox_DataBits.Items.Clear();
            toolStripComboBox_DataBits.Items.Add(5);
            toolStripComboBox_DataBits.Items.Add(6);
            toolStripComboBox_DataBits.Items.Add(7);
            toolStripComboBox_DataBits.Items.Add(8);
            toolStripComboBox_DataBits.Items.Add(9);
            toolStripComboBox_DataBits.Text = DEFAULT_DataBits.ToString();

            toolStripComboBox_Parity.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboBox_Parity.Items.Clear();
            //foreach (Parity xParity in (Parity[])Enum.GetValues(typeof(Parity)))
            foreach (string xParity in Enum.GetNames(typeof(Parity)))
            {
                toolStripComboBox_Parity.Items.Add(xParity);
            }
            toolStripComboBox_Parity.Text = DEFAULT_Parity.ToString();

            toolStripComboBox_StopBits.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboBox_StopBits.Items.Clear();
            foreach (string xStopBits in Enum.GetNames(typeof(StopBits)))
            {
                toolStripComboBox_StopBits.Items.Add(xStopBits);
            }
            toolStripComboBox_StopBits.Text = DEFAULT_StopBits.ToString();

            toolStripComboBox_Handshake.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboBox_Handshake.Items.Clear();
            foreach (string xHandshake in Enum.GetNames(typeof(Handshake)))
            {
                toolStripComboBox_Handshake.Items.Add(xHandshake);
            }
            toolStripComboBox_Handshake.Text = DEFAULT_Handshake.ToString();

            toolStripTextBox_ReadTimeout_ms.Text = DEFAULT_ReadTimeout_ms.ToString();
            toolStripTextBox_WriteTimeout_ms.Text = DEFAULT_WriteTimeout_ms.ToString();
        }

        public void AllowChanges(bool aEnabled)
        {
            comboBox_SerialPort.Enabled = aEnabled;
        }

        public static SerialPort Initialize_SerialPortDefault(SerialPort aSerialPort = null)
        {
            if (aSerialPort == null)
            {
                aSerialPort = new SerialPort();
            }
            SerialPortCount++;
            aSerialPort.PortName = "Not set " + SerialPortCount.ToString();
            aSerialPort.BaudRate = (int)DEFAULT_BaudRate;
            aSerialPort.Parity = DEFAULT_Parity;
            aSerialPort.DataBits = DEFAULT_DataBits;
            aSerialPort.StopBits = DEFAULT_StopBits;
            aSerialPort.Handshake = DEFAULT_Handshake;
            aSerialPort.ReadTimeout = (int)DEFAULT_ReadTimeout_ms;
            aSerialPort.WriteTimeout = (int)DEFAULT_WriteTimeout_ms;
            return aSerialPort;
        }

        public void SetSerialPort(SerialPort aSerialPort, bool aSetDefault)
        {
            SerialPort = ((aSerialPort == null) || aSetDefault) ? Initialize_SerialPortDefault() : aSerialPort;
            ToolTipSerialPortConfig.SetToolTip(button_SerialPortConfig, GetSerialPortConfig(SerialPort));
        }

        public void SerialPortClose()
        {
            comboBox_SerialPort.Text = MENU_ITEM_CLOSE;
        }

        protected void LogLn(string aFormat, params object[] aArgs)
        {
            if (LogString != null)
            {
                LogString(aFormat + Environment.NewLine, aArgs);
            }
        }

        private void comboBox_SerialPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SerialPort != null)
            {
                if (SerialPort.IsOpen || (SerialPort.PortName != comboBox_SerialPort.Text))
                {
                    try
                    {
                        if (comboBox_SerialPort.Text == MENU_ITEM_CLOSE)
                        {
                            if (SerialPort.IsOpen)
                            {
                                SerialPort.Close();
                                ShowInfo(SerialPort.PortName + " is " + (SerialPort.IsOpen ? "opened" : "closed"));
                            }
                            SerialPort.PortName = "Not set";
                        }
                        else
                        {
                            if (SerialPort.PortName != comboBox_SerialPort.Text)
                            {
                                if (SerialPort.IsOpen)
                                {
                                    SerialPort.Close();
                                    ShowInfo(SerialPort.PortName + " is " + (SerialPort.IsOpen ? "opened" : "closed"));
                                }
                                SerialPort.PortName = comboBox_SerialPort.Text;
                                SerialPort.Open();
                                ShowInfo(
                                    SerialPort.IsOpen
                                    ? SerialPort.PortName + " is opened (" + SerialPort.BaudRate.ToString() + "bps)"
                                    : "Com port is closed");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        comboBox_SerialPort.Text = MENU_ITEM_CLOSE;
                        SerialPort.PortName = "Not set";
                        ShowInfo(ex.Message.ToString());
                    }
                }
            }
            button_SerialPortConfig.Enabled = !SerialPort.IsOpen;
        }

        private void panel_EmuGUI_ComPort_Resize(object sender, EventArgs e)
        {
            // to debug: label_Message.MaximumSize = new Size(120, 0);
            //label_Message.MaximumSize = new Size(
            //        (button_ShowLog.Visible ? button_ShowLog.Location.X : panel_EmuGUI_ComPort.Width)
            //        - label_Message.Location.X, 0);
            //richTextBox_Info.Text = 
        }

        delegate void Delegate_ShowInfo(string aMessage);
        public void ShowInfo(string aMessage)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Delegate_ShowInfo(ShowInfo), aMessage);
                }
                else
                {
                    //string xStr = label_Message.Text;
                    //string[] xLines = xStr.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    //label_Message.Text = (xLines.Length == 0 ? "" : xLines[0]) + Environment.NewLine + aMessage;
                    LogLn((string.IsNullOrEmpty(Descriptopn) ? "" : Descriptopn + ": ") + aMessage);
                    //label_Message.Text = aMessage;
                    richTextBox_Info.Text = Descriptopn + Environment.NewLine + aMessage;
                }
            }
            catch
            {

            }
        }

        private void comboBox_SerialPort_DropDown(object sender, EventArgs e)
        {
            string xText = comboBox_SerialPort.Text;
            comboBox_SerialPort.Items.Clear();
            comboBox_SerialPort.Items.AddRange(SerialPort.GetPortNames());
            comboBox_SerialPort.Items.Insert(0, MENU_ITEM_CLOSE);
            foreach (object x in comboBox_SerialPort.Items)
            {
                if (x.ToString() == xText)
                {
                    comboBox_SerialPort.SelectedItem = x;
                }
            }
        }

        private void button_ShowLog_Click(object sender, EventArgs e)
        {
            if (EvHandler != null)
            {
                EvHandler(sender, e);
            }
        }

        private void ucSerialPort_BackColorChanged(object sender, EventArgs e)
        {
            panelFill.BackColor = BackColor;
            richTextBox_Info.BackColor = BackColor;
        }

        private void button_SerialPort_Click(object sender, EventArgs e)
        {
            toolStripComboBox_BdRate.Text = SerialPort.BaudRate.ToString();
            toolStripComboBox_DataBits.Text = SerialPort.DataBits.ToString();
            toolStripComboBox_Parity.Text = SerialPort.Parity.ToString();
            toolStripComboBox_StopBits.Text = SerialPort.StopBits.ToString();
            toolStripComboBox_Handshake.Text = SerialPort.Handshake.ToString();
            toolStripTextBox_ReadTimeout_ms.Text = SerialPort.ReadTimeout.ToString();
            toolStripTextBox_WriteTimeout_ms.Text = SerialPort.WriteTimeout.ToString();

            contextMenuStrip_SerialPortConfig.Show(button_SerialPortConfig.PointToScreen(new Point(-label_cSerialPort.Width, 0))); // button_SerialPort.Height)));
        }

        private void toolStripMenuItem_SerialPortConfig_OK_Click(object sender, EventArgs e)
        {
            ToolTipSerialPortConfig.SetToolTip(button_SerialPortConfig, "");
            try
            {
                UInt32 xBaudRate = Convert.ToUInt32(toolStripComboBox_BdRate.Text, 10);
                UInt32 xDataBits = Convert.ToUInt32(toolStripComboBox_DataBits.Text, 10);
                Parity xParity = (Parity)Enum.Parse(typeof(Parity), toolStripComboBox_Parity.Text);
                StopBits xStopBits = (StopBits)Enum.Parse(typeof(StopBits), toolStripComboBox_StopBits.Text);
                Handshake xHandshake = (Handshake)Enum.Parse(typeof(Handshake), toolStripComboBox_Handshake.Text);
                Int32 xReadTimeout_ms = Convert.ToInt32(toolStripTextBox_ReadTimeout_ms.Text, 10);
                Int32 xWriteTimeout_ms = Convert.ToInt32(toolStripTextBox_WriteTimeout_ms.Text, 10);
                try
                {
                    SerialPort.BaudRate = (int)xBaudRate;
                    SerialPort.DataBits = (int)xDataBits;
                    SerialPort.Parity = xParity;
                    SerialPort.StopBits = xStopBits;
                    SerialPort.Handshake = xHandshake;
                    SerialPort.ReadTimeout = xReadTimeout_ms < 0 ? -1 : xReadTimeout_ms;
                    SerialPort.WriteTimeout = xWriteTimeout_ms < 0 ? -1 : xWriteTimeout_ms;
                    ToolTipSerialPortConfig.SetToolTip(button_SerialPortConfig, GetSerialPortConfig(SerialPort));
                }
                catch (Exception ex1)
                {
                    MessageBox.Show(ex1.Message.ToString(), "Invalid serial port property", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ToolTipSerialPortConfig.SetToolTip(button_SerialPortConfig, "Invalid serial port property");
                }
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message.ToString(), "Conversion error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ToolTipSerialPortConfig.SetToolTip(button_SerialPortConfig, "Conversion error");
            }
        }

        private string GetSerialPortConfig(SerialPort aSerialPort)
        {
            if (aSerialPort == null)
            {
                return "Serial port is null";
            }
            else
            {
                return
                    aSerialPort.BaudRate.ToString() + ", " +
                    aSerialPort.DataBits.ToString() + ", " +
                    aSerialPort.Parity.ToString() + ", " +
                    aSerialPort.StopBits.ToString() + ", " +
                    aSerialPort.Handshake.ToString() + ", " +
                    aSerialPort.ReadTimeout.ToString() + ", " +
                    aSerialPort.WriteTimeout.ToString();
            }
        }

    } // class

} // namespace
