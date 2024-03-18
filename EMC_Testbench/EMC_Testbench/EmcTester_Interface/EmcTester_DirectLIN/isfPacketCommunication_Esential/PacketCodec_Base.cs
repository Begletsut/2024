using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace isfPacketComm.Protocol
{
    abstract public class PacketCodec_Base
    {

        public delegate void Delegate_OnReceiveItem(PacketCodec_Base aProtocol, byte[] aBytes);

        public static UInt16 QUEUE_RECV_PACKETS_MAX_SIZE = 20000; // automatic dequeue old packets

        protected List<byte[]> ListBytes { get; private set; }  // raw buffers similar in content to the received data (used only in DecodePacketAfterReceive - not reentrant)
        protected ConcurrentQueue<byte[]> queueReceivedPacks { get; private set; }  // received packets decoded from raw buffers according to the protocol

        protected Delegate_OnReceiveItem OnReceiveItem = null;

        public string LastError { get; protected set; }

        private string FimeName_DebugLog;

        /// <summary>
        /// abstract constructor, Philips Healthcare - C# Coding Standard (7@105)
        /// </summary>
        protected PacketCodec_Base(string aFimeName_DebugLog = null)
        {
            FimeName_DebugLog = aFimeName_DebugLog != null ? aFimeName_DebugLog : this.GetType().FullName + ".Log";
            ListBytes = new List<byte[]>();
            Purge();
            DebugLogFile_WriteLn("Create protocol " + GetType().FullName);
        }

        public void SubscribeOnRecv(Delegate_OnReceiveItem aOnReceiveItem)
        {
            OnReceiveItem = aOnReceiveItem;
        }

        protected void HandleReceivePacket(byte[] aPacket)
        {
            if (aPacket != null)
            {
                if (aPacket.Length > 0)
                {
                    if (OnReceiveItem != null)
                    {
                        OnReceiveItem(this, aPacket);
                    }
                    else
                    {
                        // automatic dequeue old packets
                        while (queueReceivedPacks.Count > QUEUE_RECV_PACKETS_MAX_SIZE)
                        {
                            if (!queueReceivedPacks.TryDequeue(out byte[] trash))
                            {
                                break;
                            }
                        }
                        queueReceivedPacks.Enqueue(aPacket);
                    }
                }
            }
        }

        public virtual void Purge()
        {
            ListBytes.Clear();
            queueReceivedPacks = new ConcurrentQueue<byte[]>();
            LastError = null;
        }

        /// <summary>
        /// Encode pack to send
        /// </summary>
        /// <param name="aBytes"></param>
        /// <param name="aPacket"></param>
        /// <returns></returns>
        public abstract bool EncodePacketBeforeSend(byte[] aBytes, out byte[] aPacket);

        /// <summary>
        /// Processing of newly received bytes. Call every time a low level receives a bytes
        /// </summary>
        /// <param name="aBytes"></param>
        /// <returns></returns>
        public abstract void DecodePacketAfterReceive(byte[] aBytes);

        /// <summary>
        /// Get next receive packet if exist
        /// </summary>
        /// <param name="aBuf"></param>
        /// <returns></returns>
        public bool GetNextReceivePack(out byte[] aPack)
        {
            if (!queueReceivedPacks.IsEmpty)
            {
                return queueReceivedPacks.TryDequeue(out aPack);
            }
            else
            {
                aPack = null;
                return false;
            }
        }

        protected void SetError(string aFormat, params Object[] aArgs)
        {
            LastError = string.Format(aFormat, aArgs);
            DebugLogFile_WriteLn(LastError);
        }

        protected void DebugLogFile_WriteLn(string aFormat = null, params Object[] aArgs)
        {
            string xLog = string.IsNullOrEmpty(aFormat) ? Environment.NewLine :
                string.Format("{0} : {1}{2}",
                    DateTime.Now.ToString("yyyy-MM-dd, HH:mm:ss.fff"),
                    string.Format(aFormat, aArgs),
                    Environment.NewLine);

            Console.WriteLine(xLog);
            if (FimeName_DebugLog != null)
            {
                try
                {
                    File.AppendAllText(FimeName_DebugLog, xLog);
                }
                catch
                {
                }
            }
        }


        /* * * * * * *   T E S T   *  * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
         *               T E S T 
        * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        protected abstract bool TestProtocol(bool aFirst);

        static public bool Test(PacketCodec_Base aProtocol, UInt16 aBufLenMin, UInt16 aBufLenMax, UInt16 aCyclePerLen)
        {
            aProtocol.DebugLogFile_WriteLn("===========================================");
            aProtocol.DebugLogFile_WriteLn("TEST of class " + aProtocol.GetType().Name);
            aProtocol.DebugLogFile_WriteLn("===========================================");

            bool xResult = true;
            UInt16 xLen = aBufLenMin;
            while (xLen < aBufLenMax)
            {

                aProtocol.Purge();
                xLen++;

                bool xFirst = true;
                int x = 0;
                while (x < aCyclePerLen)
                {
                    x++;
                    if (!aProtocol.TestProtocol(xFirst))
                    {
                        xResult = false;
                        break;
                    }
                    xFirst = false;
                }

                if ((xLen % 8) == 0)
                {
                    Console.WriteLine();
                }
                Console.Write(" L:{0} #:{1} ", xLen, x);
            }

            aProtocol.DebugLogFile_WriteLn("");
            aProtocol.DebugLogFile_WriteLn("");
            aProtocol.DebugLogFile_WriteLn("FINISHED: {0}", xResult ? "Complete" : "FAILED");
            aProtocol.DebugLogFile_WriteLn("");
            aProtocol.DebugLogFile_WriteLn("");
            return xResult;
        }

        protected bool TestProtocol_SendReceive(byte[] aBufToSend)
        {
            byte[] xBytes;

            while (GetNextReceivePack(out xBytes))
            {
                if (xBytes != null)
                {
                    Console.WriteLine("ERROR: A, len={0},", xBytes.Length);
                    return false;
                }
            }

            EncodePacketBeforeSend(aBufToSend, out xBytes);
            DecodePacketAfterReceive(xBytes);
            GetNextReceivePack(out xBytes);
            if (xBytes != null)
            {
                if (Encoding.ASCII.GetString(xBytes) != Encoding.ASCII.GetString(aBufToSend))
                {
                    Console.WriteLine("ERROR: C: recv {0} != {1}", xBytes.Length, BitConverter.ToString(xBytes), BitConverter.ToString(aBufToSend));
                    return false;
                }
            }
            else
            {
                Console.WriteLine("ERROR: D: xBytes is null");
            }
            return true;
        }


    } // class

} // namespace
