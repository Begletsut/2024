
using System;
using System.Linq;

namespace EmcTester_Interface
{
    public class EmcTester_Device
    {
        private UInt16 TimeOut_ToDisconnected_State_ms = 1500;

        public enum State_EMC
        {
            Disconnected,
            Ready,
            TestInProgress,
            TestedOK,
            TestedError
        }
        public enum State_Tester
        {
            NotInit,
            Error,
            Disconnected,
            Ready                 // after Tester power On
        }

        public object Tag = null;

        public DateTime DtTmFirst { get; private set; } = default;
        public DateTime DtTmLast { get; private set; } = default;

        public byte TesterId { get; private set; }
        public State_EMC StateEMC { get; private set; } = State_EMC.Disconnected;
        
        private State_Tester mStateTester = State_Tester.NotInit;
        public State_Tester StateTester  // true: EMC tester is connected.
        {
            get
            {
                DateTime xDtTm = DateTime.Now;
                if ((xDtTm - DtTmLast) > TimeSpan.FromMilliseconds(TimeOut_ToDisconnected_State_ms))
                {
                    mStateTester = State_Tester.Disconnected;
                }
                return mStateTester;
            }
            set 
            { 
                if (value == State_Tester.Ready)
                {
                    DtTmLast = DateTime.Now;
                    if (DtTmFirst < DtTmLast)
                    {
                        DtTmFirst = DtTmLast;
                    }
                }
                mStateTester = value; 
            }
        }
        public UInt32 PackNum { get; private set; } = 0; // received packet number
        public bool Power { get; private set; } = false; // true = OK, false = Error
        public bool Oxygen { get; private set; } = false; // true = OK, false = Error
        public bool RPM { get; private set; } = false;  // true = OK, false = Error
        public bool StepMotor { get; private set; } = false;  // true = OK, false = Error
        public bool Keyboard { get; private set; } = false;  // true = OK, false = Error
        public bool LIN_MT05 { get; private set; } = false;  // true = OK, false = Error
        public bool LIN_CO { get; private set; } = false;  // true = OK, false = Error
        public bool POut { get; private set; } = false;  // true = OK, false = Error
        public bool FRAM { get; private set; } = false;  // true = OK, false = Error
        public bool StateOnOff { get; private set; } = false; // true = ON, false = OFF
        public bool TypeEngine { get; private set; } = false;
        public byte BootloaderVersion { get; private set; } = 0;
        public byte AppVersion { get; private set; } = 0;

        public EmcTester_Device(byte aTesterId)
        {
            TesterId = aTesterId;
            StateTester = State_Tester.Ready;
            PackNum = 0;
        }

        public override string ToString()
        {
            string xResult = string.Format("Id:{0,3}, Tester:{1,-16}, EMC:{2,-16} {3,8}#:  ", TesterId, StateTester + ",", StateEMC + ",", PackNum);

            string xInvalid = "";
            if (!Power)
            {
                xInvalid += " Power";
            }

            if (!Oxygen)
            {
                xInvalid += " Oxy";
            }
            if (!RPM)
            {
                xInvalid += " RPM";
            }
            if (!StepMotor)
            {
                xInvalid += " StepMot";
            }
            if (!Keyboard)
            {
                xInvalid += " Kbd";
            }
            if (!LIN_MT05)
            {
                xInvalid += " MT05";
            }
            if (!LIN_CO)
            {
                xInvalid += " CO";
            }
            if (!POut)
            {
                xInvalid += " Pout";
            }
            if (!FRAM)
            {
                xInvalid += " FRAM";
            }
            if (!StateOnOff)
            {
                xInvalid += " Sonoff";
            }
            if (!TypeEngine)
            {
                xInvalid += " TypeEngine";
            }
            if (BootloaderVersion == 0)
            {
                xInvalid += " BtldVer";
            }
            if (AppVersion == 0)
            {
                xInvalid += " AppVer";
            }
            if (string.IsNullOrEmpty(xInvalid))
            {
                xInvalid = "successful";
            }
            else
            {
                xInvalid = "UNSUCCESSFUL: " + xInvalid;
            }
            xResult += xInvalid;

            return xResult;
        }

        public bool Update(byte[] aMsg)
        {

            //            internal bool ReceivePack(byte[] data)
            //            {
            //                //DateTime xNow = DateTime.Now;
            //                //if (!Control.Enabled)
            //                //{
            //                //    DtTmFirst = xNow;
            //                //}
            //                //DtTmLast = xNow;

            //                //Control.Enabled = true;

            //                //if ((data == null) || (data.Length == 0))
            //                //{
            //                //    State = TesterState.Ready;
            //                //    return true;
            //                //}
            //                //else if (data[0] <= Enum.GetValues(typeof(TesterState)).Length)
            //                //{
            //                //    State = (TesterState)data[0];
            //                //    return true;
            //                //}
            //                return false;
            //            }

            PackNum++;
            StateTester = State_Tester.Ready;
            if ((aMsg != null) && (aMsg.Length > 0))
            {
                byte xCurrentState = (byte)((aMsg[1] & 0xF0) >> 4);
                if (xCurrentState <= (byte)(Enum.GetValues(typeof(State_EMC)).Cast<State_EMC>().Last()))
                {
                    StateEMC = (State_EMC)xCurrentState;
                    Power = (aMsg[1] & 0x08) > 0;
                    Oxygen = (aMsg[1] & 0x04) > 0;
                    RPM = (aMsg[1] & 0x02) > 0;
                    StepMotor = (aMsg[1] & 0x01) > 0;
                    Keyboard = (aMsg[2] & 0x80) > 0;
                    LIN_MT05 = (aMsg[2] & 0x40) > 0;
                    LIN_CO = (aMsg[2] & 0x20) > 0;
                    POut = (aMsg[2] & 0x10) > 0;
                    FRAM = (aMsg[2] & 0x08) > 0;
                    StateOnOff = (aMsg[2] & 0x04) > 0;
                    TypeEngine = (aMsg[2] & 0x02) > 0;
                    BootloaderVersion = aMsg[3];
                    AppVersion = aMsg[4];
                    return true;
                }
            }
            return true;
        }

    } // class

} // namespace
