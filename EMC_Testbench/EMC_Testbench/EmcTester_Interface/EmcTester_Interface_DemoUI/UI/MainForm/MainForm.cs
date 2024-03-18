using System;
using System.Linq;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace EmcTester_Interface_DemoUI
{

    public partial class MainForm : Form
    {
        private const byte DEVICES_COUNT_MAX = 120;

        private byte[] Bytes_ToSend = null;

        private static object[] comboBox_SendData_Items =
            {
                "02 02",
                "02 03 D0 1",
                "02 04 1 1",
                "02 05",
                "02 06 07"
            };
        private static string comboBox_SendData_Help =
                comboBox_SendData_Items[0] + ": Test keyboard" + Environment.NewLine +
                comboBox_SendData_Items[1] + ": Set lamda [D0](80-DC) to time [1] sec" + Environment.NewLine +
                comboBox_SendData_Items[2] + ": Set RPM (type 0/1/2/3) to time [1] sec" + Environment.NewLine +
                comboBox_SendData_Items[3] + ": Test step motor" + Environment.NewLine +
                comboBox_SendData_Items[4] + ": [07]: bit 0x01: MotorType (1:Vg); 0x02: RPM (1:Kw;0:Ko); 0x04: OxyOnOffPWM; 0x10: EnPout.";


        public MainForm()
        {
            InitializeComponent();

            Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            FormPriliminaryTest_ADC_Grid.Icon = Icon;
            FormPriliminaryTest_ADC_Log.Icon = Icon;

            ToSend_Init();
            Communicator_Init();
            Emulator_Init();

            if (Communicator.IsValid)
            {
                timerUI.Start();
            }
            else
            {
                button_CAN_CommunicatorShow.BackColor = Color.LightPink;
                comboBox_SendData.BackColor = Color.LightPink;
            }

            comboBox_SendData.Items.AddRange(comboBox_SendData_Items);

            comboBox_SendData_TextChanged(null, null);
            // checkBox_Devices_SelectALL.Checked = true; TODO FIX this: select ALL, but not send comboBox_SendData CAN message
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Communicator_Deinit();
            Emulator_Deinit();
        }

        private void panel_Devices_Resize(object sender, EventArgs e)
        {
            ToSend_Resize();
        }

        private void button_Devices_PowerOn_Click(object sender, EventArgs e)
        {
            Command_CreateAndSend(Communicator_CmdPowerOn);
        }

        private void button_Devices_PowerOff_Click(object sender, EventArgs e)
        {
            Command_CreateAndSend(Communicator_CmdPowerOff);
        }

        private void button_Devices_Reset_Click(object sender, EventArgs e)
        {
            Command_CreateAndSend(Communicator_CmdReset);
        }

        private void button_Devices_StartTest_Click(object sender, EventArgs e)
        {
            Command_CreateAndSend(Communicator_CmdStartTest);
        }

        private void button_Debug_Click(object sender, EventArgs e)
        {
            Command_CreateAndSend(Communicator_CmdDebug);
        }

        private void button_AllDevices_PowerOn_Click(object sender, EventArgs e)
        {
            Communicator_CmdPowerOn(null);
        }

        private void button_AllDevices_PowerOff_Click(object sender, EventArgs e)
        {
            Communicator_CmdPowerOff(null);
        }

        private void button_AllDevices_Reset_Click(object sender, EventArgs e)
        {
            Communicator_CmdReset(null);
        }

        private void button_AllDevices_StartTest_Click(object sender, EventArgs e)
        {
            Communicator_CmdStartTest(null);
        }

        private void button_AllDevices_Debug_Click(object sender, EventArgs e)
        {
            Communicator_CmdDebug(null);
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (splitContainer1.SplitterDistance < 160)
            {
                splitContainer1.SplitterDistance = 160;
            }
        }

        private void checkBox_Devices_SelectALL_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Devices_SelectALL.Checked)
            {
                ToSend_CheckAll(true);
                checkBox_Devices_SelectNone.Checked = false;
            }
        }

        private void checkBox_Devices_SelectNone_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Devices_SelectNone.Checked)
            {
                ToSend_CheckAll(false);
                checkBox_Devices_SelectALL.Checked = false;
            }
        }

        private void button_Devices_UpdateInfo_Clear_Click(object sender, EventArgs e)
        {
            FromRecv_Clear();
        }

        private void button_Devices_UpdateInfo_Get_Click(object sender, EventArgs e)
        {
            FromRecv_Update();
        }

        private void button_CAN_CommunicatorShow_Click(object sender, EventArgs e)
        {
            Communicator_Show();
        }

        private void button_CAN_EmulatorShow_Click(object sender, EventArgs e)
        {
            Emulator_Show();
        }

        private void button_CAN_EmulatorSendRandom_Click(object sender, EventArgs e)
        {
            Emulator_SendRandom();
        }

        private void button_PreliminaryTests_Click(object sender, EventArgs e)
        {
            if (sender is Control)
            {
                contextMenuStrip_PreliminaryTests.Show(((Control)sender).PointToScreen(new Point(0, ((Control)sender).Height)));
            }
        }

        private void toolStripMenuItem_PreliminaryTests_ADC_Click(object sender, EventArgs e)
        {
            PreliminaryTests_ADC_Grid();
        }

        private void showTestADCLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreliminaryTests_ADC_Log();
        }

        private void timerUI_Tick(object sender, EventArgs e)
        {
            ToSend_TimerUI_Tick();
        }

        private void button_Send_Click(object sender, EventArgs e)
        {
            Command_CreateAndSend(Communicator_SendPack);
        }

        private void comboBox_SendData_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Bytes_ToSend = comboBox_SendData.Text
                    .Trim()
                    .Split(' ')                               // Split into items 
                    .Select(item => Convert.ToByte(item, 16)) // Convert each item into byte
                    .ToArray();
            }
            catch
            {
                Bytes_ToSend = null;
            }

            if ((Bytes_ToSend != null) && (Bytes_ToSend.Length <= 8))
            {
                comboBox_SendData.BackColor = SystemColors.Window;
                button_Send.Enabled = true;
            }
            else
            {
                comboBox_SendData.BackColor = Color.LightPink;
                button_Send.Enabled = false;
            }
        }

        private void button_Send_Help_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                comboBox_SendData_Help,
                "Help of CAN send test command:",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
        }

    } // class 

} // namespace
