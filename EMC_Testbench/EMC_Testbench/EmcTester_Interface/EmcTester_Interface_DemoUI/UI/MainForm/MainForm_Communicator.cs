
using System;
using System.Text;
using System.Windows.Forms;

using CAM_CANdriver_2;
using EmcTester_Interface;


namespace EmcTester_Interface_DemoUI
{

    // MainForm Communicator
    public partial class MainForm : Form
    {

        private EmcTester_Communicator Communicator;

        private void Communicator_Init()
        {
            Communicator = new EmcTester_Communicator(this, Communicator_Update, Communicator_ReceiveHandler);
            Communicator_Show();
        }

        private void Communicator_Deinit()
        {
            Communicator?.Deinit();
        }

        private void Communicator_Show()
        {
            Communicator?.Show();
        }

        private bool Communicator_ReceiveHandler(CAN_DATA_STRUC aCanMsg)
        {
            if (PreliminaryTests_ReceiveHandler(aCanMsg))
            {
                return true;
            }

            //return FromReceive_ReceiveHandler(aCanMsg); // if handle messages in owner app (here)
            return false;
        }

        private void Communicator_Update(EmcTester_Device aDevice)
        {
            if (aDevice != null)
            {
                Control xControl = aDevice.Tag is Control ?
                    (Control)aDevice.Tag :
                    ToSend_GetControlById(aDevice.TesterId);
                if (xControl is CheckBox)
                {
                    ((CheckBox)xControl).Enabled = true;
                }
            }
        }

        private bool Communicator_CmdPowerOn(byte[] aTesterIds)
        {
            if (Communicator == null) 
            {
                return false;
            }
            else
            {
                if ((aTesterIds == null) || (aTesterIds.Length == 0))
                {
                    return Communicator.Send_CmdPowerOn(0);
                }
                else
                {
                    foreach (byte xId in aTesterIds)
                    {
                        if (!Communicator.Send_CmdPowerOn(xId))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool Communicator_CmdPowerOff(byte[] aTesterIds)
        {
            if (Communicator == null)
            {
                return false;
            }
            else
            {
                if ((aTesterIds == null) || (aTesterIds.Length == 0))
                {
                    return Communicator.Send_CmdPowerOff(0);
                }
                else
                {
                    foreach (byte xId in aTesterIds)
                    {
                        if (!Communicator.Send_CmdPowerOff(xId))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool Communicator_CmdReset(byte[] aTesterIds)
        {
            if (Communicator == null)
            {
                return false;
            }
            else
            {
                if ((aTesterIds == null) || (aTesterIds.Length == 0))
                {
                    return Communicator.Send_CmdReset(0);
                }
                else
                {
                    foreach (byte xId in aTesterIds)
                    {
                        if (!Communicator.Send_CmdReset(xId))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool Communicator_CmdStartTest(byte[] aTesterIds)
        {
            if (Communicator == null)
            {
                return false;
            }
            else
            {
                if ((aTesterIds == null) || (aTesterIds.Length == 0))
                {
                    return Communicator.Send_CmdStartTest(0);
                }
                else
                {
                    foreach (byte xId in aTesterIds)
                    {
                        if (!Communicator.Send_CmdStartTest(xId))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool Communicator_CmdDebug(byte[] aTesterIds)
        {
            if (Communicator == null)
            {
                return false;
            }
            else
            {
                if ((aTesterIds == null) || (aTesterIds.Length == 0))
                {
                    return Communicator.Send_CmdStartTest(0);
                }
                else
                {
                    foreach (byte xId in aTesterIds)
                    {
                        if (!Communicator.Send_CmdDebug(xId))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private string Communicator_GetDevicesInfo()
        {
            string xResult = null;
            if (Communicator != null)
            {
                StringBuilder xSB = new StringBuilder();
                for (byte xId = 0; xId < DEVICES_COUNT_MAX; xId++)
                {
                    EmcTester_Device xDevice = Communicator.GetDevice(xId);
                    if (xDevice != null)
                    {
                        xSB.AppendLine(xDevice.ToString());
                    }
                }
                if (xSB.Length > 0)
                {
                    xResult = xSB.ToString();
                }
            }
            return xResult;
        }

        private EmcTester_Device Communicator_GetDeviceById(byte aId)
        {
            return Communicator == null?null: Communicator.GetDevice(aId);
        }

        private bool Communicator_SendPack(byte[] aTesterIds)
        {
            if (Communicator == null)
            {
                return false;
            }
            else
            {
                if ((aTesterIds == null) || (aTesterIds.Length == 0))
                {
                    return Communicator.Send_CmdPowerOn(0);
                }
                else
                {
                    foreach (byte xId in aTesterIds)
                    {
                        if (!Communicator.Send_CAN(xId, Bytes_ToSend))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }


    } // class

} // namespace
