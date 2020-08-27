using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworkLib.Enums;
using NetworkLib.Network;

namespace UTest_Client
{
    public partial class Form1 : Form
    {
        private static IPAddress IP { get; set; } = IPAddress.Parse("127.0.0.1");
        private static int Port { get; set; } = 8008;

        private static List<string> mpt = new List<string>()
        {
            "System",
            "Error",
            "Information",
            "Data",
            "Message"
        };

        private static List<string> spt = new List<string>()
        {
            "Request",
            "Responce",
            "Click",
            "isAlive",
            "Die"
        };

        private static List<string> ppt = new List<string>();

        NetworkStream ns;

        public Form1()
        {
            InitializeComponent();
            packetPType_cb.DataSource = mpt;
            packetType_cb.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void connect_btn_Click(object sender, EventArgs e)
        {
            
        }

        private void packetType_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(packetType_cb.SelectedIndex)
            {
                case 0:
                    packetPType_cb.DataSource = mpt;
                    break;
                case 1:
                    packetPType_cb.DataSource = spt;
                    break;
                case 2:
                    packetPType_cb.DataSource = ppt;
                    packetPType_cb.Text = null;
                    break;
            }
        }

        private void send_btn_Click(object sender, EventArgs e)
        {
            TcpClient client = new TcpClient();
            client.Connect(IP, Port);
            ns = client.GetStream();
            connect_lbl.Text = client.Client.Connected.ToString();
            PacketService ps = new PacketService(Encoding.Default.GetBytes(data_tb.Text), false);
            ps.SetPacketType((PType.PacketType)Enum.Parse(typeof(PType.PacketType), packetType_cb.Text));

            if(packetType_cb.SelectedIndex == 0) ps.SetPacketInformationType((PType.MessagePacketType)Enum.Parse(typeof(PType.MessagePacketType), packetPType_cb.Text));
            else if(packetPType_cb.SelectedIndex == 1) ps.SetPacketInformationType((PType.SessionPacketType)Enum.Parse(typeof(PType.SessionPacketType), packetPType_cb.Text));

            Packet p = ps.MakePacket();
            ns.Write(p.BData, 0, p.BData.Length);
            responce_lb.Items.Add(Encoding.Default.GetString(p.BData));
            ns.Close();
            client.Close();
        }
    }
}
