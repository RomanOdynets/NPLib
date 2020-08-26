using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace NetworkLib.Crypt
{
    public static class CryptEngine
    {
        public static RSAParameters publicKey { get; private set; }
        public static RSAParameters privateKey { get; private set; }
        public static RSACryptoServiceProvider RSA { get; private set; }

        public static string serializedPublic { get; private set; }

        static CryptEngine()
        {
            RSA = new RSACryptoServiceProvider(1024);
            publicKey = RSA.ExportParameters(false);
            privateKey = RSA.ExportParameters(true);
            Serialize();
        }

        private static void Serialize()
        {
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, publicKey);
            serializedPublic = sw.ToString();
        }

        public static byte[] Encrypt(byte[] msg, string XMLPublicKey)
        {
            using (var r = new RSACryptoServiceProvider(256))
            {
                try
                {
                    r.FromXmlString(XMLPublicKey);

                    var enryptData = r.Encrypt(msg, true);

                    var base64Encr = Convert.ToBase64String(enryptData);

                    return Encoding.Default.GetBytes(base64Encr);
                }
                finally
                {
                    r.PersistKeyInCsp = false;
                }
            }
        }

        public static string Decrypt(string text)
        {
            using (var r = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    var base64 = text;

                    r.ImportParameters(privateKey);

                    var res = Convert.FromBase64String(base64);
                    var decrB = r.Decrypt(res, true);
                    var decrD = Encoding.Default.GetString(decrB);
                    return decrD.ToString();
                }
                finally
                {
                    r.PersistKeyInCsp = false;
                }
            }
        }
    }
}
