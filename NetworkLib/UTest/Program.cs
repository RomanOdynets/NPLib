using System;
using NetworkLib.Packets;
using NetworkLib.Enums;
using System.Text;
using NetworkLib.Crypt;

namespace UTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Packet p = new Packet(Console.ReadLine());
            p.MakeCrypted(true);
            p.SetPacketType(PType.PacketType.Message);
            p.SetPacketInfomartionType(PType.MessagePacketType.Data);

            Console.WriteLine(Encoding.Default.GetString(p.GetPacket()));
            Main(null);
        }
    }
}
