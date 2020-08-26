using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using NetworkLib.Crypt;
using NetworkLib.Enums;

namespace NetworkLib.Packets
{
    public class Packet
    {
        public PType.MessagePacketType mpt { get; private set; }
        public PType.SessionPacketType spt { get; private set; }
        public PType.PacketType pt { get; private set; }

        private string mdata { get; set; }
        public byte[] data { get; private set; }

        public bool isCrypt { get; private set; }
        public RSACryptoServiceProvider RSA { get; private set; }

        public Packet(string mdata)
        {
            #region PacketInform To Class Fields
            this.mdata = mdata;
            #endregion
        }

        public Packet(byte[] data)
        {
            this.data = data;
        }

        public void SetPacketType(PType.PacketType type) => pt = type;

        public bool SetPacketInfomartionType(PType.MessagePacketType mpt)
        {
            if (pt != PType.PacketType.Message) return false;
            this.mpt = mpt;
            return true;
        }

        public bool SetPacketInformationType(PType.SessionPacketType spt)
        {
            if (pt != PType.PacketType.Session) return false;
            this.spt = spt;
            return true;
        }

        public void MakeCrypted(bool isCrypt)
        {
            this.isCrypt = isCrypt;
            this.RSA = CryptEngine.RSA;
        }

        public bool Parse(IPAddress iPAddress = null)
        {
            string tmp = Encoding.Default.GetString(data);
            bool crypted = true;
            if (tmp.Contains('|')) crypted = false;
            
            if(crypted)
            {
                string message = CryptEngine.Decrypt(tmp);
                string[] buffer = message.Split('|');
                pt = (PType.PacketType)int.Parse(buffer[0]);

                if(pt == PType.PacketType.Message)
                {
                    mpt = (PType.MessagePacketType)int.Parse(buffer[1]);
                    mdata = buffer[2];

                    return true;
                }

                spt = (PType.SessionPacketType)int.Parse(buffer[1]);
                mdata = null;
                return true;
            }
            else
            {
                string[] buffer = tmp.Split('|');
                if (buffer.Length == 2)
                {
                    pt = PType.PacketType.PublicKey;
                    PublicKeys.keys.Add(iPAddress, buffer[1]);

                    return true;
                }
            }

            return true;
        }

        public byte[] GetPacket()
        {
            if (isCrypt && pt != PType.PacketType.PublicKey) data = CryptEngine.Encrypt(Construct(), CryptEngine.serializedPublic);
            else data = Construct();

            if (data.Length > 0) return data;
            else throw new Exception("Packet Lenght equals 0");
        }

        private byte[] Construct()
        {
            string tmp = string.Empty;
            switch(pt)
            {
                case PType.PacketType.PublicKey:
                    tmp = $"{pt}|{CryptEngine.serializedPublic}";
                    break;
                case PType.PacketType.Session:
                    tmp = $"{pt}|{spt}|{string.Empty}";
                    break;
                case PType.PacketType.Message:
                    tmp = $"{pt}|{mpt}|{mdata}";
                    break;
            }

            return Encoding.Default.GetBytes(tmp);
        }
    }
}
