
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using isf_canterm_basic;
using CAM_CANdriver_2;

namespace EmcTester_Interface
{
    public class EmcTester_Communicator
    {
        public bool IsValid { get { return can_terminal != null; } }

        private const byte TESTER_CAN_USER_PDU = 0xEF;

        private const byte TESTER_CAN_HOST_PC_ADDR = 0x00;
        //private const byte TESTER_CAN_TESTER_ADDR = 0x10;
        private const byte TESTER_CAN_HOST_PC_PRIO = 0x00;
        private const byte TESTER_CAN_TESTER_PRIO = 0x01;

        //private const byte TESTER_CAN_HOST_PC_CMD_POWER_OFF = 0x00;
        //private const byte TESTER_CAN_HOST_PC_CMD_POWER_ON = 0x01;
        //private const byte TESTER_CAN_HOST_PC_CMD_RESET = 0x02;
        //private const byte TESTER_CAN_HOST_PC_CMD_START_TEST = 0x03;

        private const byte TESTER_CAN_TESTER_CMD_INFO = 0x00;

        private const UInt32 TESTER_COMMAND_MASK_RECV = (UInt32)((TESTER_CAN_HOST_PC_PRIO << 26) | (TESTER_CAN_USER_PDU << 16) | (TESTER_CAN_HOST_PC_ADDR));
        private const UInt32 TESTER_COMMAND_MASK_SEND = (UInt32)((TESTER_CAN_HOST_PC_PRIO << 26) | (TESTER_CAN_USER_PDU << 16) | (TESTER_CAN_HOST_PC_ADDR << 8));

        //// 0x4EF0010, 82771984
        //const UInt32 CmdTesterInfo = (UInt32)((TESTER_CAN_TESTER_PRIO << 26) | (TESTER_CAN_USER_PDU << 16) | (TESTER_CAN_TESTER_CMD_INFO << 8)); // | (TESTER_CAN_TESTER_ADDR));

        private const UInt16 TIMEOUT_TO_DISCONNECTED_STATE_ms = 1500;

        private Form_ISFCANterm can_terminal;
        private Dictionary<byte, EmcTester_Device> Devices = new Dictionary<byte, EmcTester_Device>();

        Func<CAN_DATA_STRUC, bool> OwnerHandler_CAN = null;
        Action<EmcTester_Device> Communicator_Update = null;

        public EmcTester_Communicator(Form Owner, Action<EmcTester_Device> aCommunicator_Update, Func<CAN_DATA_STRUC, bool> aOwnerHandler_CAN)
        {
            can_terminal = new Form_ISFCANterm("EMC tester communicator");
            if (can_terminal.bError)
            {
                can_terminal = null;
            }
            else
            {
                can_terminal.OwnerFormApp = Owner;
                can_terminal.OwnerHandler_CAN = This_Handler_CAN;
                OwnerHandler_CAN = aOwnerHandler_CAN;
                Communicator_Update = aCommunicator_Update;
            }
        }

        public void Deinit()
        {
            if (IsValid)
            {
                can_terminal.OwnerFormApp = null;
                can_terminal.Close();
                can_terminal = null;
            }
        }

        public void Show()
        {
            if (IsValid)
            {
                can_terminal.Hide();
                can_terminal.Show();
            }
        }

        public EmcTester_Device GetDevice(byte aIdDev)
        {
            if (Devices.TryGetValue(aIdDev, out EmcTester_Device xResult))
            {
                return xResult;
            }
            return null;
        }

        public bool Send_CmdPowerOff(byte aTesterId)
        {
            return Send_CAN(aTesterId, new byte[] { 1, 0 });
        }

        public bool Send_CmdPowerOn(byte aTesterId)
        {
            return Send_CAN(aTesterId, new byte[] { 1, 1 });
        }

        public bool Send_CmdReset(byte aTesterId)
        {
            return Send_CAN(aTesterId, new byte[] { 1, 2 });
        }

        public bool Send_CmdStartTest(byte aTesterId)
        {
            return Send_CAN(aTesterId, new byte[] { 1, 3 });
        }

        public bool Send_CmdDebug(byte aTesterId)
        {
            return Send_CAN(aTesterId, new byte[] { 1, 0x80 });
        }

        public void Get_CmdTesterInfo_Random(out UInt32 aId, out byte[] aData)
        {
            // TODO make new

            //Random xRnd = new Random();
            //aId = CmdTesterInfo;
            //aData = new byte[5];
            //xRnd.NextBytes(aData);
            //aData[0] = (byte)((aData[0] & 0x0F) + 1); // Set emulated Id between 0..15
            //aData[1] = (byte)(aData[1] & 0x3F); // Set emulated state between 0..3
            aId = 0;
            aData = null;
        }

        public bool Send_CAN(byte aTesterId, byte[] aData)
        {
            if (IsValid)
            {
                return can_terminal.Send(new CAN_DATA_STRUC()
                {
                    id = TESTER_COMMAND_MASK_SEND | aTesterId,
                    data = aData,
                    numBytes = (byte)(aData == null ? 0 : aData.Length)
                });
            }
            return false;
        }

        private bool This_Handler_CAN(UInt32 aNextMsgNum, CAN_DATA_STRUC aCanMsg)
        {
            bool xComplete = OwnerHandler_CAN == null ? false : OwnerHandler_CAN(aCanMsg);

            if (xComplete)
            {
                if (GetSourceAddr(aCanMsg.id, out byte xTesterId))
                {
                    if (!Devices.TryGetValue(xTesterId, out EmcTester_Device xDevice))
                    {
                        xDevice = new EmcTester_Device(xTesterId);
                        Devices.Add(xTesterId, xDevice);
                    }
                    if (xDevice != null)
                    {
                        if (xDevice.Update(aCanMsg.data))
                        {
                            if (Communicator_Update != null)
                            {
                                Communicator_Update(xDevice);
                            }
                        }
                    }
                }
            }
            return true;
        }

        private bool GetSourceAddr(UInt32 aId, out byte aTesterId)
        {
            if (((aId >> 26) & 0x7) == TESTER_CAN_TESTER_PRIO)
            {
                if (((aId >> 16) & 0xFF) == TESTER_CAN_USER_PDU)
                {
                    if ((aId & 0xFF) == TESTER_CAN_HOST_PC_ADDR)
                    {
                        aTesterId = (byte)((aId >> 8) & 0xFF);
                        return true;
                    }
                }
            }
            aTesterId = 0xFF;
            return false;
        }

    } // class

} // namespace
