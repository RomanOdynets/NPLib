using System;

namespace NetworkLib.Enums
{
    public class PType
    {
        public enum PacketType
        {
            Message = 0,
            Session,
            PublicKey
        }

        public enum MessagePacketType
        {
            System = 0,
            Error,
            Information,
            Data,
            Message
        }

        public enum SessionPacketType
        {
            Request = 0,
            Responce,
            Check,
            isAlive,
            Die
        }
    }
}
