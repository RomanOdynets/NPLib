using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using NetworkLib.Crypt;
using NetworkLib.Enums;

namespace NetworkLib.Network
{
    public class PacketService
    {
        private Packet packet { get; set; }
        
        private byte[] Data { get; set; }
        private bool IsCrypt { get; set; }

        public PacketService(byte[] Data, bool IsCrypt)
        {
            this.Data = Data;
            this.IsCrypt = IsCrypt;
            this.packet = new Packet();
        }

        public void SetPacketType(PType.PacketType type) => packet.PT = type;
        public void SetPacketInformationType(PType.MessagePacketType mtype) => packet.MPT = (packet.PT == PType.PacketType.Message) ? mtype : 0;
        public void SetPacketInformationType(PType.SessionPacketType stype) => packet.SPT = (packet.PT == PType.PacketType.Session) ? stype : 0;
        
        public Packet MakePacket(string publicKey = null)
        {
            if (publicKey == null) publicKey = CryptEngine.serializedPublic;

            if (IsCrypt && packet.PT != PType.PacketType.PublicKey)
                packet.BData = CryptEngine.Encrypt(ConstructPacket(), publicKey);
            else
                packet.BData = ConstructPacket();

            if (packet.BData.Length > 0) return packet;
            else throw new Exception("Bytes Lenght was equals zero.");
        }

        public static Packet GetPacket(byte[] data)
        {
            Packet temp = new Packet();

            string buffer = Encoding.Default.GetString(data);
            bool crypted = true;
            if (buffer.Contains('|')) crypted = false;

            if (crypted)
            {
                string message = CryptEngine.Decrypt(buffer);
                string[] array = message.Split('|');

                temp.PT = (PType.PacketType)Enum.Parse(typeof(PType.PacketType), array[0]);

                if (temp.PT == PType.PacketType.Message)
                {
                    temp.MPT = (PType.MessagePacketType)Enum.Parse(typeof(PType.MessagePacketType), array[1]);
                    temp.SData = array[2] ?? null;
                }
                else if(temp.PT == PType.PacketType.Session)
                {
                    temp.SPT = (PType.SessionPacketType)Enum.Parse(typeof(PType.SessionPacketType), array[1]);
                    temp.SData = null;
                }
            }
            else
            {
                string[] array = buffer.Split('|');

                try
                {
                    temp.PT = (PType.PacketType)Enum.Parse(typeof(PType.PacketType), array[0]);
                }
                catch
                {
                    if (array[1].Length == 172)
                    {
                        temp.PT = PType.PacketType.PublicKey;
                    }
                }

                if (temp.PT == PType.PacketType.PublicKey)
                {
                    string pkey_temp = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\n" +
                        "<RSAParameters xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\n" +
                        "\t<Exponent>AQAB</Exponent>\n" +
                        "\t<Modulus>" + array[1] + "</Modulus>\n" +
                        "</RSAParameters>";
                }
                else if (temp.PT == PType.PacketType.Message)
                {
                    temp.MPT = (PType.MessagePacketType)Enum.Parse(typeof(PType.MessagePacketType), array[1]);
                    temp.SData = array[2];
                }
                else if (temp.PT == PType.PacketType.Session)
                {
                    temp.SPT = (PType.SessionPacketType)Enum.Parse(typeof(PType.SessionPacketType), array[1]);
                    temp.SData = null;
                }
            }

            return temp;
        }

        private byte[] ConstructPacket()
        {
            string tmp = string.Empty;
            switch (packet.PT)
            {
                case PType.PacketType.PublicKey:
                    tmp = $"{GetPublicBlock()}";
                    break;
                case PType.PacketType.Session:
                    tmp = $"{packet.PT}|{packet.SPT}|{string.Empty}";
                    break;
                case PType.PacketType.Message:
                    tmp = $"{packet.PT}|{packet.MPT}|{Encoding.Default.GetString(this.Data)}";
                    break;
            }

            return Encoding.Default.GetBytes(tmp);
        }

        private string GetPublicBlock()
        {
            Console.WriteLine(CryptEngine.serializedPublic);
            var serializator = new XmlSerializer(typeof(PKeys));
            using(TextReader reader = new StringReader(CryptEngine.serializedPublic.Replace("RSAParameters", "PKeys")))
            {
                packet.Key = (PKeys)serializator.Deserialize(reader);
            }

            return packet.Key.Modulus;
        }

        private string MakeXMLPublicKey()
        {
            return null;
        }
    }
}
