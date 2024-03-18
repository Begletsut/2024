using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

using MySqlConnector;

using EmcTester_Interface;
using CAM_CANdriver_2;

namespace EMC_Testbench
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        EmcTester_Communicator Communicator;
        public Dictionary<int, UserControl1> userControls = new Dictionary<int, UserControl1>();
        DBForm dBForm = new DBForm();
        public Form1()
        {
            InitializeComponent();
            Communicator = new EmcTester_Communicator(this, Communicator_Update, Communicator_ReceiveHandler);
            Communicator.Show();
            setUserControlComunnicator(this);
        }

        private void setUserControlComunnicator(Control aControl)
        {
            foreach (Control xControl in aControl.Controls)
            {
                if (xControl is UserControl1)
                {
                    UserControl1 currentControl = (UserControl1)xControl;
                    currentControl.SetCommunictor(Communicator);
                    userControls.Add(currentControl.Id, currentControl);
                }
                else
                {
                    setUserControlComunnicator(xControl);
                }
            }
        }

        private void Communicator_Update(EmcTester_Device aDevice)
        {
            if (!(aDevice.Tag is UserControl1))
            {
                if (userControls.ContainsKey(aDevice.TesterId))
                {
                    aDevice.Tag = userControls[aDevice.TesterId];
                    userControls[aDevice.TesterId].Device = aDevice;
                }
            }
            if (aDevice.Tag is UserControl1)
            {
                ((UserControl1)aDevice.Tag).UpdateEMCInfo();
            }
        }

        private bool Communicator_ReceiveHandler(CAN_DATA_STRUC aCanMsg)
        {
            //if (PreliminaryTests_ReceiveHandler(aCanMsg))
            //{
            //    return true;
            //}

            ////return FromReceive_ReceiveHandler(aCanMsg); // if handle messages in owner app (here)
            //return false;
            return true;
        }


        public void exportToDocxToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("SN\tAinVpower\tKeyboard\tBootloaderVersion\tAppVersion\tEngine\tOxygen\tRPM\tPOut\tLINMT05\tLINCO\tStepMotor\tFlash\tDate");
            foreach (UserControl1 userControl in userControls.Values)
            {
                stringBuilder.AppendLine($"{userControl.valueSN.Text}\t{userControl.valuePower.Text}\t{userControl.valueKeyboard.Text}\t{userControl.valueBootloader.Text}\t{userControl.valueApp.Text}\t{userControl.valueEngine.Text}\t{userControl.valueOxygen.Text}\t{userControl.valueRPM.Text}\t{userControl.valuePOut.Text}\t{userControl.valueMT05.Text}\t{userControl.valueCO.Text}\t{userControl.valueStepM.Text}\t{userControl.valueFRAM.Text}\t{DateTime.Now}");
            }

            File.WriteAllText(Path.Combine(Application.StartupPath, "current.txt"), stringBuilder.ToString());
            MessageBox.Show("Successfully exported!");
        }

        private void saveToDBToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            foreach (UserControl1 control in userControls.Values)
            {
                string query = $@"INSERT INTO testedemc SET 
                    SN = '{control.valueSN.Text}',
                    AinVPower = '{control.valuePower.Text}',
                    Keyboard = '{control.valueKeyboard.Text}',
                    BootloaderVersion = '{control.valueBootloader.Text}',
                    AppVersion = '{control.valueApp.Text}',
                    Engine = '{control.valueEngine.Text}',
                    Oxygen = '{control.valueOxygen.Text}',
                    RPM = '{control.valueRPM.Text}',
                    POut = '{control.valuePOut.Text}',
                    LINMT05 = '{control.valueMT05.Text}',
                    LINCO = '{control.valueCO.Text}',
                    StepMotor = '{control.valueStepM.Text}',
                    Flash = '{control.valueFRAM.Text}',
                    Date = '{DateTime.Now}';";
                MySqlCommand cmd = new MySqlCommand(query, dBForm.connection);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }

            dBForm.connection.Close();
            dBForm.connDB();
            MessageBox.Show("Successfully inserted!");
        }

        private void openTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dBForm.Show();
        }

        private void powerONAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Communicator.Send_CmdPowerOn(0);
        }

        private void powerOFFAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Communicator.Send_CmdPowerOff(0);
        }
        private void restartAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Communicator.Send_CmdReset(0);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Communicator.Deinit();
        }

    } // class
    public class Device_EMCtester
    {
        public enum State
        {
            Disconnected,
            TestInProgress,
            TestedOK,
            TestedError,
        }
        public State CurrentState { get; private set; }
        public UInt16 TesterId { get; private set; }
        public bool Power { get; private set; } // true = OK, false = Error
        public bool Oxygen { get; private set; } // true = OK, false = Error
        public bool RPM { get; private set; } // true = OK, false = Error
        public bool StepMotor { get; private set; } // true = OK, false = Error
        public bool Keyboard { get; private set; } // true = OK, false = Error
        public bool LINMT05 { get; private set; } // true = OK, false = Error
        public bool LINCO { get; private set; } // true = OK, false = Error
        public bool POut { get; private set; } // true = OK, false = Error
        public bool Fram { get; private set; } // true = OK, false = Error
        public bool StateOnOff { get; private set; }// true = ON, false = OFF
        public string BootloaderVersion { get; private set; }
        public string AppVersion { get; private set; }
        public string TypeEngine { get; private set; }
        public string SN { get; private set; }
    } // class

} // namespace