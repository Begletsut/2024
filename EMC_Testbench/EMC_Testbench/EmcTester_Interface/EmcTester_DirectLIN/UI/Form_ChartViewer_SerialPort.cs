using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

using isfPacketComm;
using isfPacketComm.Protocol;
using isf_SerialPort_Config;

namespace EMC20_emuLambdaControl
{
    // part Serial port
    public partial class Form_ChartViewer : Form
    {
        private UInt32 PacketCount_OK = 0;
        private UInt32 PacketCount_Error = 0;

        private ucSerialPort_Config ucSerPort;
        private PacketCommunicator_SerialPort Communicator;
        private PacketCodec_EOL Codec;
        private int Tick0 = 0;

        private string SerialPort_FileNameLog; 

        public void SerialPort_Init()
        {
            string xDtTmStart = DateTime.Now.ToString("yyyy-MM-dd,HHmmss");
            SerialPort_FileNameLog = AppTitle + " " + xDtTmStart + ".xls";
            File.AppendAllText(SerialPort_FileNameLog, "//==========================================================================================================================" + Environment.NewLine);
            File.AppendAllText(SerialPort_FileNameLog, "//    " + xDtTmStart + Environment.NewLine);
            File.AppendAllText(SerialPort_FileNameLog, "//==========================================================================================================================" + Environment.NewLine);
            File.AppendAllText(SerialPort_FileNameLog, "//-- \t ----- \t --- \t ------ \t ------ \t --------- \t ------- \t --------- \t --------- \t --------- \t --------" + Environment.NewLine);
            File.AppendAllText(SerialPort_FileNameLog, "// # \t ticks \t RPM \t OxyADC \t OxyAvg \t OxyFilter \t DltDiff \t DltFilter \t DltIntegt \t StM_Speed \t StM_Pos" + Environment.NewLine);
            File.AppendAllText(SerialPort_FileNameLog, "//-- \t ----- \t --- \t ------ \t ------ \t --------- \t ------- \t --------- \t --------- \t --------- \t --------" + Environment.NewLine);

            label_PackCount.Text = "- - -";
            SerialPort xSerPort = new SerialPort();
            xSerPort.BaudRate = 19200;
            ucSerPort = new ucSerialPort_Config(AppTitle, xSerPort);
            ucSerPort.Init(null, null, null, SystemColors.Control, AppTitle, false);

            Codec = new PacketCodec_EOL();
            Communicator = new PacketCommunicator_SerialPort("EMC test oxy", Codec, ucSerPort.SerialPort);

            panel_SerialPort.Controls.Add(ucSerPort);
            ucSerPort.Dock = DockStyle.Top;
            panel_SerialPort.Height = ucSerPort.Height + ucSerPort.Top;
            ucSerPort.Dock = DockStyle.Fill;

        }

        private void SerialPort_Update()
        {
            if (ucSerPort.SerialPort.IsOpen)
            {
                if (checkBox_Emu.Checked)
                {
                    Communicator.SendPack(Encoding.ASCII.GetBytes(">1900:0900."));
                }
                else
                {
                    Communicator.SendPack(Encoding.ASCII.GetBytes(">."));
                }
            }

            if (Communicator.RecvNext(out byte[] xBytes))
            {
                if ((xBytes != null) && (xBytes.Length > 0) && (xBytes[0] == (byte)':'))
                {
                    Console.WriteLine("  :  " + DateTime.Now.ToString("HH:mm:ss.fff") + ":  " + BitConverter.ToString(xBytes));

                    if (checkBox_Run.Checked)
                    {
                        if (SerialPort_GetPoints(Encoding.ASCII.GetString(xBytes), out UInt32[] xPoins))
                        {
                            Chart_AddSample(xPoins);
                        }
                    }
                }
            }
        }

        private bool SerialPort_GetPoints(string aPacket, out UInt32[] aPoints)
        {
            // xPacket = ":0,1>1,0,0,700,700,627,0,0."

            aPoints = null;

            if (string.IsNullOrEmpty(aPacket))
            {
                SerialPort_ShowInfo("Is empty");
                return false;
            }

            if ((aPacket[0] != ':') && (aPacket[aPacket.Length] != '.') && (aPacket.IndexOf('>') < 0))
            {
                SerialPort_ShowInfo("Format error");
                return false;
            }

            aPacket = aPacket.Remove(aPacket.Length - 1);
            aPacket = aPacket.Remove(0, 1);
            aPacket = aPacket.Replace('>', ',');
            string[] xStrFields = aPacket.Split(',');
            if (xStrFields.Length != 10)
            {
                SerialPort_ShowInfo("Fields count error");
                return false;
            }

            try
            {
                aPoints = Array.ConvertAll(xStrFields, s => UInt32.Parse(s));
                PacketCount_OK++;
                label_PackCount.Text = PacketCount_OK.ToString();
                if (Tick0==0)
                {
                    Tick0 = Environment.TickCount;
                }
                File.AppendAllText(
                    SerialPort_FileNameLog, 
                    string.Format("{0,-8}\t{1,-10}\t{2}{3}",
                    PacketCount_OK,
                    (UInt32)(Environment.TickCount - Tick0),
                    aPacket.Replace(',', '\t'),Environment.NewLine));
                return true;
            }
            catch (Exception ex)
            {
                SerialPort_ShowInfo("Fields format error");
                return false;
            }

            //aPoints = new UInt32[] { 100, 200, 300, 400, 500, 600, 700, 0 };
            //aPoints[3] = x800++;
            //return true;

            ////aPoins = null;
            ////if (charTimeIndexEmu < charTimeIndex)
            ////{
            ////    aPoins = new uint[6];
            ////    int i = 0;
            ////    while (i < 6)
            ////    {
            ////        //aPoins[i] = chartData[i][charTimeIndexEmu];
            ////        i++;
            ////    }
            ////    charTimeIndexEmu++;
            ////}
            ////return aPoins != null;
        }

        private void SerialPort_ShowInfo(string aInfo)
        {
            PacketCount_Error++;
            ucSerPort.ShowInfo("Packet decode: " + aInfo + " (" + PacketCount_Error.ToString() + ").");
        }

    } // class

} // namespace
