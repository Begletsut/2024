using System;
using System.Threading.Tasks;
using System.Windows.Forms;

using EmcTester_Interface;

namespace EMC_Testbench
{
    public partial class UserControl1 : UserControl
    {
        private EmcTester_Communicator Communicator;
        public byte Id;
        public EmcTester_Device Device { get; set; }
        public UserControl1()
        {
            InitializeComponent();
            UpdateCounterLabel();
            metroTabControl1.SelectedIndex = 0;
            byte.TryParse(labelCounter.Text, out Id);
        }
        public void UpdateCounterLabel()
        {
            int count = Counter.GetNextCount();
            labelCounter.Text = count.ToString();
        }
        public void UpdateEMCInfo()
        {
            labelValueState.Text = Device.StateEMC.ToString();
            labelValueTester.Text = Device.StateTester.ToString();
            valuePower.Text = Device.Power.ToString();
            valueOxygen.Text = Device.Oxygen.ToString();
            valueRPM.Text = Device.RPM.ToString();
            valueStepM.Text = Device.StepMotor.ToString();
            //valueKeyboard.Text = Device.Keyboard.ToString();
            //valueMT05.Text = Device.LIN_MT05.ToString();
            //valueCO.Text = Device.LIN_CO.ToString();
            //valuePOut.Text = Device.POut.ToString();
            //valueFRAM.Text = Device.FRAM.ToString();
        }
        private void btnPowerON_Click(object sender, System.EventArgs e)
        {
            Communicator.Send_CmdPowerOn(Id);
        }

        private void btnPowerOFF_Click(object sender, System.EventArgs e)
        {
            Communicator.Send_CmdPowerOff(Id);
        }

        private void btnReset_Click(object sender, System.EventArgs e)
        {
            Communicator.Send_CmdReset(Id);
        }

        private void btnTest_Click(object sender, System.EventArgs e)
        {
            Communicator.Send_CmdStartTest(Id);
        }

        internal void SetCommunictor(EmcTester_Communicator aCommunicator)
        {
            Communicator = aCommunicator;
        }

        private async void checkBoxAutoTest_CheckedChanged(object sender, EventArgs e)
        {
            while (checkBoxAutoTest.Checked)
            {
                Communicator.Send_CmdStartTest(Id);
                await Task.Delay(5000);
            }
        }
    }// class

    public static class Counter
    {
        public static int _count = 0;
        public static int GetNextCount()
        {
            return ++ _count;
        }
    }// class
}// namespace
