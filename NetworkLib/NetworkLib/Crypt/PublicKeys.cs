using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NetworkLib.Crypt
{
    static class PublicKeys
    {
        public static Dictionary<IPAddress, string> keys = new Dictionary<IPAddress, string>();
    }
}
