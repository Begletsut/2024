
using System;
using System.IO.Ports;

using isfPacketComm.Protocol;

namespace isfPacketComm
{

    public class PacketCommunicator_SerialPort : PacketCommunicator_Base
    {
        public SerialPort SerialPort { get; protected set; }

        public override bool IsOpen { get { return SerialPort == null ? false : SerialPort.IsOpen; } } // IComTerm not have a "Started" property

        public PacketCommunicator_SerialPort(string aName, PacketCodec_Base aProtocol, SerialPort aSerialPort) : base(aName, aProtocol)
        {
            SerialPort = aSerialPort;
            SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceiveHandler);
        }

        public override bool Send(byte[] aBytes)
        {
            if (aBytes == null)
            {
                LastError = "Send(), bytes is null";
            }
            else
            {
                try
                {
                    if (IsOpen)
                    {
                        SerialPort.Write(aBytes, 0, aBytes.Length);
                        return true;
                    }
                    else
                    {
                        LastError = "SerialPort is not open";
                    }
                }
                catch (Exception ex)
                {
                    LastError = ex.Message.ToString();
                }
            }
            return false;
        }

        private void SerialPort_DataReceiveHandler(object sender, SerialDataReceivedEventArgs e)
        {
            if ((sender is SerialPort) && (sender == SerialPort))
            {
                if (SerialPort.BytesToRead > 0)
                {
                    byte[] xBytes = new byte[SerialPort.BytesToRead];
                    int xLen = SerialPort.Read(xBytes, 0, xBytes.Length);
                    if (xLen != xBytes.Length)
                    {
                        LastError = "ERROR: private void SerialPort_DataReceiveHandler(object sender, SerialDataReceivedEventArgs e)";
                        //Console.WriteLine("ERROR: private void SerialPort_DataReceiveHandler(object sender, SerialDataReceivedEventArgs e)");
                    }
                    // Console.WriteLine(BitConverter.ToString(xBytes));
                    // Console.WriteLine(System.Text.ASCIIEncoding.ASCII.GetString(xBytes));
                    AfterReadBytes(xBytes);
                }
            }
        }

        public override bool Start(string aName, int aBaudRate)
        {
            try
            {
                SerialPort.PortName = aName;
                SerialPort.BaudRate = aBaudRate;
                SerialPort.Open();
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message.ToString();
                return false;
            }
        }

        public override bool Stop()
        {
            try
            {
                SerialPort.Close();
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message.ToString();
                return false;
            }
        }

    } // class

} // namespace

