using System;
using System.Text;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

using isfPacketComm;
using isfPacketComm.Protocol;
using isf_SerialPort_Config;

namespace EmcTester_DirectLIN
{
    // part Serial port
    public partial class Form_DirectLIN : Form
    {
        private UInt32 PacketCount_ALL = 0;
        private UInt32 PacketCount_OK = 0;
        private UInt32 PacketCount_Error = 0;

        private ucSerialPort_Config ucSerPort;
        private PacketCommunicator_SerialPort Communicator;
        private PacketCodec_Base Codec;
        private int Tick0 = 0;

        private string SerialPort_FileNameLog;

        private bool SerialPort_TestLoop_Run = false;

        public void SerialPort_Init()
        {

            SerialPort_InitLogFile();

            SerialPort xSerPort = new SerialPort();
            xSerPort.BaudRate = 10400;
            ucSerPort = new ucSerialPort_Config(AppTitle, xSerPort);
            ucSerPort.Init(null, null, null, SystemColors.Control, AppTitle, false);

            Codec = new PacketCodec_Bin();
            Communicator = new PacketCommunicator_SerialPort("EMC test oxy", Codec, ucSerPort.SerialPort);

            panel_SerialPort.Controls.Add(ucSerPort);
            ucSerPort.Dock = DockStyle.Top;
            panel_SerialPort.Height = ucSerPort.Height + ucSerPort.Top;
            ucSerPort.Dock = DockStyle.Fill;

        }

        //public void SerialPort_SetCommunicator_Bin()
        //{
        //    Codec = new PacketCodec_Bin();
        //    Communicator = new PacketCommunicator_SerialPort("EMC test oxy", Codec, ucSerPort.SerialPort);
        //}

        //public void SerialPort_SetCommunicator_Line()
        //{
        //    Codec = new PacketCodec_EOL();
        //    Communicator = new PacketCommunicator_SerialPort("EMC test oxy", Codec, ucSerPort.SerialPort);
        //}

        private void SerialPort_InitLogFile()
        {
            string xDtTmStart = DateTime.Now.ToString("yyyy-MM-dd,HHmmss");
            SerialPort_FileNameLog = AppTitle + " " + xDtTmStart + ".xls";
            //File.AppendAllText(SerialPort_FileNameLog, "//==========================================================================================================================" + Environment.NewLine);
            //File.AppendAllText(SerialPort_FileNameLog, "//    " + xDtTmStart + Environment.NewLine);
            //File.AppendAllText(SerialPort_FileNameLog, "//==========================================================================================================================" + Environment.NewLine);
            //File.AppendAllText(SerialPort_FileNameLog, "//-- \t ----- \t --- \t ------ \t ------ \t --------- \t ------- \t --------- \t --------- \t ----------- \t ---------- \t --------- \t --------- \t --------" + Environment.NewLine);
            //File.AppendAllText(SerialPort_FileNameLog, "// # \t ticks \t RPM \t OxyADC \t OxyAvg \t OxyFilter \t DltDiff \t DltFilter \t DltIntegt \t CtrlActMode \t CtrlAction \t StM_Mode  \t StM_Speed \t StM_Pos " + Environment.NewLine);
            //File.AppendAllText(SerialPort_FileNameLog, "//-- \t ----- \t --- \t ------ \t ------ \t --------- \t ------- \t --------- \t --------- \t ----------- \t ---------- \t --------- \t --------- \t --------" + Environment.NewLine);
        }

        private void SerialPort_Update()
        {
            //if (ucSerPort.SerialPort.IsOpen)
            //{
            //    if (checkBox_Emu.Checked)
            //    {
            //        string xEmu = string.Format(">{0:0000}:{1:0000}.", (int)numericUpDown_EmuRPM.Value, (int)numericUpDown_EmuOxy.Value);
            //        Communicator.SendPack(Encoding.ASCII.GetBytes(xEmu));
            //    }
            //    else
            //    {
            //        Communicator.SendPack(Encoding.ASCII.GetBytes(">."));
            //    }


            while (Communicator.RecvNext(out byte[] xBytes))
            {
                PacketCount_ALL++;

                richTextBox_Log.AppendText("r: " + BitConverter.ToString(xBytes) + Environment.NewLine);

                //label_PackCntALL.Text = PacketCount_ALL.ToString();
                //if ((xBytes != null) && (xBytes.Length > 0) && (xBytes[0] == (byte)':'))
                //{
                //    Console.WriteLine("  :  " + DateTime.Now.ToString("HH:mm:ss.fff") + ":  " + BitConverter.ToString(xBytes));

                //    if (checkBox_Run.Checked)
                //    {
                //        if (SerialPort_GetPoints(Encoding.ASCII.GetString(xBytes), out Int32[] xPoins))
                //        {
                //            Chart_AddSample(xPoins);
                //        }
                //    }
                //}
            }
        }

        private bool SerialPort_SendPack(byte[] aBytes)
        {
            richTextBox_Log.AppendText("s: " + BitConverter.ToString(aBytes) + Environment.NewLine);
            return ucSerPort.SerialPort.IsOpen ?
                Communicator.SendPack(aBytes) :
                false;
        }

        private void SerialPort_SetEmcToTestMode()
        {
            SerialPort_SendPack(new byte[] { 0x23, 0x00, 0x14, 0x34, 0x12, 0x56, 0x78 });
        }

        private bool SerialPort_SendStr(string aText)
        {
            richTextBox_Log.AppendText("s: " + aText + "\\r\\n, binary ");
            return SerialPort_SendPack(Encoding.ASCII.GetBytes(aText + "\r\n"));

        }


        private void SerialPort_Count_Clear()
        {
            PacketCount_ALL = 0;
            PacketCount_OK = 0;
            PacketCount_Error = 0;
        }

    } // class

} // namespace
