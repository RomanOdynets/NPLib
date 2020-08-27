using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace NetworkLib.Crypt
{
    public static class PublicKeys
    {
        public static Dictionary<IPAddress, string> keys = new Dictionary<IPAddress, string>();
    }

    [XmlRoot]
    public class PKeys
    {
        [XmlElement]
        public string Exponent { get; set; }
        [XmlElement]
        public string Modulus { get; set; }
        public PKeys() { }
    }
}
