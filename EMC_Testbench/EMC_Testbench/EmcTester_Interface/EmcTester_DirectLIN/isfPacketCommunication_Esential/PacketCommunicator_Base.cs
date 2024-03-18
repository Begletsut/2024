using System;

using isf_core;
using isfPacketComm.Protocol;

namespace isfPacketComm
{
    public abstract class PacketCommunicator_Base
    {
        public delegate void Delegate_OnReceiveItem(PacketCommunicator_Base aCommunicator, byte[] aBytes);

        public object ConfigTag = null;

        public abstract bool IsOpen { get; }

        public string Name { get; }

        public string LastError { get; protected set; }

        public PacketCodec_Base Protocol { get; protected set; }

        protected Delegate_OnReceiveItem OnReceiveItem { get; set; }

        private OnLogString OnLogString = null;

        /// <summary>
        /// Philips Healthcare - C# Coding Standard
        /// Rule 7@105, Synopsis: Explicitly define a protected constructor on an abstract base class
        /// </summary>
        /// <param name="aName"></param>
        /// <param name="aProtocol"></param>
        protected PacketCommunicator_Base(string aName, PacketCodec_Base aProtocol)
        {
            Name = aName;
            Protocol = aProtocol;
        }

        public void Init(object aConfigTag, Delegate_OnReceiveItem aOnReceiveItem, OnLogString aOnLogString)
        {
            ConfigTag = aConfigTag;
            OnReceiveItem = aOnReceiveItem;
            OnLogString = aOnLogString;
        }

        public abstract bool Send(byte[] aBytes);

        public virtual bool SendPack(byte[] aBytes)
        {
            bool xResult = false;
            if (aBytes == null)
            {
                LastError = "SendPack(), bytes is null";
            }
            else
            {
                try
                {
                    if (Protocol == null)
                    {
                        xResult = Send(aBytes);
                    }
                    else if (Protocol.EncodePacketBeforeSend(aBytes, out byte[] xBytes))
                    {
                        xResult = Send(xBytes);
                    }
                }
                catch (Exception ex)
                {
                    LastError = ex.Message.ToString();
                }
            }
            LogString(xResult ? "Send: " + BitConverter.ToString(aBytes) : "Send err: " + LastError);
            return xResult;
        }

        /// <summary>
        /// if (OnReceiveItem != null): Get next received packet (if exist), else return false
        /// </summary>
        /// <param name="aBytes"></param>
        /// <returns></returns>
        public virtual bool RecvNext(out byte[] aBytes)
        {
            aBytes = null;
            return 
                Protocol == null ? false :
                OnReceiveItem != null ? false : Protocol.GetNextReceivePack(out aBytes);
        }

        protected void LogString(string aFormat, params object[] aArgs)
        {
            OnLogString?.Invoke(aFormat, aArgs);
        }

        //public abstract bool Connect(object aInfo);

        //public abstract bool Disconnect();

        public abstract bool Start(string aName, int aBaudRate);

        public abstract bool Stop();

        protected virtual void AfterReadBytes(byte[] aBytes)
        {
            if (Protocol != null)
            {
                Protocol.DecodePacketAfterReceive(aBytes);
                if (OnReceiveItem != null)
                {
                    while (Protocol.GetNextReceivePack(out byte[] xPacket))
                    {
                        OnReceiveItem(this, xPacket);
                    }
                }
            }
            else
            {
                if (OnReceiveItem != null)
                {
                    OnReceiveItem(this, aBytes);
                }
            }
        }

    } // class

}  // namespace
