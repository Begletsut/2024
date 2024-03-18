
using System;
using System.Windows.Forms;

using isf_canterm_basic;

namespace EmcTester_Interface_DemoUI
{

    // MainForm Emulator
    public partial class MainForm : Form
    {

        private Form_ISFCANterm can_terminal = null;

        private void Emulator_Init()
        {
            button_CAN_EmulatorSendRandom.Visible = false;
        }

        private void Emulator_Deinit()
        {
            if (can_terminal != null)
            {
                can_terminal.Close();
            }
        }

        private void Emulator_Show()
        {
            if (can_terminal == null)
            {
                can_terminal = new Form_ISFCANterm("Emulator to EMC tester");
                if (can_terminal.bError)
                {
                    can_terminal = null;
                }
                else
                {
                    can_terminal.OwnerHandler_Form = Emulator_CAN_OwnerHandler_Form;
                    button_CAN_EmulatorShow.BackColor = System.Drawing.Color.LightGreen;
                    button_CAN_EmulatorSendRandom.BackColor = System.Drawing.Color.LightGreen;
                    button_CAN_EmulatorSendRandom.Visible = true;
                    // тъпоптия, защото наследствено тая форма не работи, ако не се покаже поне веднъж.
                    int x = can_terminal.Left;
                    can_terminal.Left = -999999;
                    can_terminal.Show();
                    can_terminal.Hide();
                    can_terminal.Left = x;
                }
            }
            else // if (can_terminal != null)
            {
                can_terminal.Hide();
                can_terminal.Show();
            }
        }

        private void Emulator_CAN_OwnerHandler_Form(object sender, EventArgs e)
        {
            if ((sender == can_terminal) && (e is FormClosingEventArgs))
            {
                can_terminal = null;
                button_CAN_EmulatorShow.BackColor = System.Drawing.Color.LightPink;
                button_CAN_EmulatorSendRandom.Visible = false;
            }
        }

        private void Emulator_SendRandom()
        {
            if (can_terminal != null)
            {
                Communicator.Get_CmdTesterInfo_Random(out UInt32 xId, out byte[] xData);
                can_terminal.Send(new CAM_CANdriver_2.CAN_DATA_STRUC() { id = xId, data = xData });
            }
        }


    } // class

} // namespace
