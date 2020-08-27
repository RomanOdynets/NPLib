using NetworkLib.Crypt;
using NetworkLib.Enums;

namespace NetworkLib.Network
{
    public class Packet
    {
        public PType.PacketType PT { get; set; }
        public PType.MessagePacketType MPT { get; set; }
        public PType.SessionPacketType SPT { get; set; }

        public PKeys Key { get; set; }

        public byte[] BData { get; set; }
        public string SData { get; set; }
    }
}
