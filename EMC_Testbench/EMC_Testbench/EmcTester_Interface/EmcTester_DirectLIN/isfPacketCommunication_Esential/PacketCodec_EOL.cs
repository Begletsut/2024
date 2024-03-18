using System;
using System.Text;

namespace isfPacketComm.Protocol
{
    public class PacketCodec_EOL : PacketCodec_Base
    {
        private static byte[] EOL_RECV_DEFAULT = { 0x0D, 0x0A };
        private static byte[] EOL_SEND_DEFAULT = { 0x0D, 0x0A };

        protected byte[] EOL_Recv { get; private set; }
        protected byte[] EOL_Send { get; private set; }

        private byte EOL_RecvIndex;
        private bool flDecodeThreadBusy = false;

        public PacketCodec_EOL(byte[] aEOL_Recv, byte[] aEOL_Send, string aFimeName_ErrorLog = null) : base(aFimeName_ErrorLog)
        {
            EOL_Recv = (aEOL_Recv == null) ? EOL_RECV_DEFAULT : aEOL_Recv;
            EOL_Send = (aEOL_Send == null) ? EOL_SEND_DEFAULT : aEOL_Send;
        }

        public PacketCodec_EOL(byte[] aEOL, string aFimeName_ErrorLog = null) : this(aEOL, aEOL, aFimeName_ErrorLog)
        {

        }

        public PacketCodec_EOL(string aFimeName_ErrorLog = null) : this(null, null, aFimeName_ErrorLog)
        {

        }

        public override void Purge()
        {
            base.Purge();
            EOL_RecvIndex = 0;
            flDecodeThreadBusy = false;
        }

        public override void DecodePacketAfterReceive(byte[] aBytes)
        {
            if (aBytes != null)
            {
                while (flDecodeThreadBusy) ;
                flDecodeThreadBusy = true;

                bool xEnqueue = false;
                int xBytesIndex_PackBegin = 0; // last end of packet index
                int xBytesIndex_LastEOL = 0; // last end of packet index
                int xBytesIndex = 0; // while cycle variable
                while (xBytesIndex < aBytes.Length)
                {
                    if (aBytes[xBytesIndex] == EOL_Recv[EOL_RecvIndex])
                    {
                        xBytesIndex_LastEOL = xBytesIndex + 1;
                        EOL_RecvIndex++;
                        // is EOL received?
                        if (EOL_RecvIndex >= EOL_Recv.Length)
                        {
                            EOL_RecvIndex = 0;

                            // get length of packet
                            int xPackLen = xBytesIndex + 1 - EOL_Recv.Length - xBytesIndex_PackBegin;
                            foreach (byte[] xBytes in ListBytes)
                            {
                                xPackLen += xBytes.Length;
                            }

                            // this: byte[] xPack = new byte[xPackLen > 0 ? xPackLen : 0];
                            // or this: to debug only:
                            byte[] xPack;
                            if (xPackLen > 0)
                            {
                                xPack = new byte[xPackLen];
                            }
                            else
                            {
                                xPack = new byte[0];
                            }

                            int xLenCopy;
                            int xPackIndex = 0;
                            foreach (byte[] xBytes in ListBytes)
                            {
                                xLenCopy = (xPack.Length - xPackIndex) > xBytes.Length ? xBytes.Length : xPack.Length - xPackIndex;
                                if (xLenCopy > 0)
                                {
                                    Array.Copy(xBytes, 0, xPack, xPackIndex, xLenCopy);
                                    xPackIndex += xLenCopy;
                                }
                            }
                            xLenCopy = (xPack.Length - xPackIndex) > aBytes.Length ? aBytes.Length : xPack.Length - xPackIndex;
                            if (xLenCopy > 0)
                            {
                                Array.Copy(aBytes, xBytesIndex_PackBegin, xPack, xPackIndex, xLenCopy);
                                xPackIndex += xLenCopy;
                            }
                            xBytesIndex_PackBegin = xBytesIndex + 1;
                            xEnqueue = true;
                            HandleReceivePacket(xPack);
                            ListBytes.Clear();

                            if (xPackIndex != xPack.Length)
                            {
                                throw new InvalidProgramException("xPackIndex != xPack.Length");
                            }
                        }
                    }
                    else
                    {
                        EOL_RecvIndex = 0;
                    }
                    xBytesIndex++;
                }
                if ((!xEnqueue) || (xBytesIndex_LastEOL == 0))
                {
                    ListBytes.Add(aBytes);
                }
                else
                {
                    if (aBytes.Length > xBytesIndex_LastEOL)
                    {
                        byte[] xBytes = new byte[aBytes.Length - xBytesIndex_LastEOL];
                        Array.Copy(aBytes, xBytesIndex_LastEOL, xBytes, 0, xBytes.Length);
                        ListBytes.Add(xBytes);
                    }
                }
            }
            flDecodeThreadBusy = false;
        }

        public override bool EncodePacketBeforeSend(byte[] aBytes, out byte[] aPacket)
        {
            bool xResult = false;
            if (aBytes == null)
            {
                aPacket = null;
            }
            else
            {
                xResult = true;
                if (aBytes.Length == 0)
                {
                    aPacket = EOL_Send;
                }
                else if (EOL_Send.Length == 0)
                {
                    aPacket = aBytes;
                }
                else
                {
                    aPacket = new byte[aBytes.Length + EOL_Send.Length];
                    Array.Copy(aBytes, 0, aPacket, 0, aBytes.Length);
                    Array.Copy(EOL_Send, 0, aPacket, aBytes.Length, EOL_Send.Length);
                }
            }
            return xResult;
        }

        /* * * * * * *   T E S T   *  * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
         *               T E S T 
        * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
        protected override bool TestProtocol(bool aFirst)
        {
            string check1 = "ABCDEFG";
            string check2 = "KLMN" + check1;
            byte[] TEST_BUF_1 = { 0x41, 0x42, 0x43, 0x44, 0x45 };
            byte[] TEST_BUF_2 = { 0x46, 0x47, 0x0D };
            byte[] TEST_BUF_3 = { 0x0A, 0x4B, 0x4C, 0x4D, 0x4E };

            byte[] xBytes = new byte[] { 0x31, 0x32, 0x33, 0x41, 0x42, 0x43 };

            if (aFirst)
            {
                Purge();
                if (!TestProtocol_SendReceive(xBytes))
                {
                    Purge();
                    TestProtocol_SendReceive(xBytes);
                }
                Purge();
            }

            DecodePacketAfterReceive(TEST_BUF_1);
            while (GetNextReceivePack(out xBytes))
            {
                if (xBytes != null)
                {
                    Console.WriteLine("ERROR: A{0},", xBytes.Length);
                    return false;
                }
            }

            DecodePacketAfterReceive(TEST_BUF_2);
            while (GetNextReceivePack(out xBytes))
            {
                if (xBytes != null)
                {
                    Console.WriteLine("ERROR: B{0},", xBytes.Length);
                    return false;
                }
            }

            DecodePacketAfterReceive(TEST_BUF_3);
            if (GetNextReceivePack(out xBytes))
            {
                if (Encoding.ASCII.GetString(xBytes) != (aFirst ? check1 : check2))
                {
                    Console.WriteLine("ERROR: C{0}: recv {1} != {2}",
                        xBytes.Length, Encoding.ASCII.GetString(xBytes), aFirst ? check1 : check2);
                    return false;
                }
            }
            while (GetNextReceivePack(out xBytes))
            {
                Console.WriteLine("ERROR: C{0}: error, receive unwanted: {1}",
                    xBytes.Length, Encoding.ASCII.GetString(xBytes));
                return false;
            }

            return true;
        }

    } // class

} // namespace
