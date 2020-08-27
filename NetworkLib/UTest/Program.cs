using System;
using System.Net;
using System.Text;
using NetworkLib.Enums;
using NetworkLib.Network;
using NetworkLib.Crypt;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace UTest
{
    class Program
    {
        private static IPAddress IP { get; set; } = IPAddress.Parse("127.0.0.1");
        private static int Port { get; set; } = 8008;
        private static TcpListener listener { get; set; } = new TcpListener(IP, Port);

        static void Main(string[] args)
        {
            listener.Start();
            Console.WriteLine("Wait for connections...");
            Listen();
            Console.ReadLine();
        }

        private async static void Listen()
        {
            await Task.Run(() =>
            {
                while(true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClientAsync().Result;
                        Console.WriteLine($"Client was connected");
                        NetworkStream nw = client.GetStream();

                        StringBuilder sb = new StringBuilder();
                        int bytes = 0;
                        byte[] buffer = new byte[1024];
                        do
                        {
                            bytes = nw.Read(buffer, 0, buffer.Length);
                        } while (nw.DataAvailable);

                        IPEndPoint ep = (IPEndPoint)client.Client.RemoteEndPoint;
                        Packet packet = PacketService.GetPacket(buffer);
                        string ptype = packet.PT == PType.PacketType.Message ? packet.MPT.ToString() : packet.SPT.ToString();
                        Console.WriteLine($"{ep.Address} => [{packet.PT}] [{ptype}] {packet.SData}");
                    }
                    catch
                    {
                        continue;
                    }
                    
                }
            });
        }
    }
}
