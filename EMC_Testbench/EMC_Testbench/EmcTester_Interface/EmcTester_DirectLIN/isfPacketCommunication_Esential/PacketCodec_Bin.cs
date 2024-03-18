using System;
using System.Text;

namespace isfPacketComm.Protocol
{
    public class PacketCodec_Bin : PacketCodec_Base
    {
        public PacketCodec_Bin(string aFimeName_ErrorLog = null) : base(aFimeName_ErrorLog)
        {
        }

        public override void DecodePacketAfterReceive(byte[] aBytes)
        {
            HandleReceivePacket(aBytes);
        }

        public override bool EncodePacketBeforeSend(byte[] aBytes, out byte[] aPacket)
        {
            aPacket = aBytes;
            return aPacket != null;
        }

        /* * * * * * *   T E S T   *  * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
         *               T E S T 
        * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
        protected override bool TestProtocol(bool aFirst)
        {
            byte[] TEST_BUF_1 = { 0x41, 0x42, 0x43, 0x44, 0x45 };
            byte[] TEST_BUF_2 = { 0x46, 0x47, 0x0D };
            byte[] TEST_BUF_3 = { 0x0A, 0x4B, 0x4C, 0x4D, 0x4E };

            if (!TestProtocolBuf(TEST_BUF_1))
                return false;
            if (!TestProtocolBuf(TEST_BUF_2))
                return false;
            if (!TestProtocolBuf(TEST_BUF_3))
                return false;
            return true;
        }

        private bool TestProtocolBuf(byte[] aBufToSend)
        {
            byte[] xBytes;

            while (GetNextReceivePack(out xBytes))
            {
                if (xBytes != null)
                {
                    Console.WriteLine("ERROR: A{0},", xBytes.Length);
                    return false;
                }
            }

            DecodePacketAfterReceive(aBufToSend);

            if (GetNextReceivePack(out xBytes))
            {
                if (Encoding.ASCII.GetString(xBytes) != Encoding.ASCII.GetString(aBufToSend))
                {
                    Console.WriteLine("ERROR: C{0}: recv {1} != {2}",
                        xBytes.Length, Encoding.ASCII.GetString(xBytes), Encoding.ASCII.GetString(aBufToSend));
                    return false;
                }
            }
            return true;
        }

    } // class

} // namespace
