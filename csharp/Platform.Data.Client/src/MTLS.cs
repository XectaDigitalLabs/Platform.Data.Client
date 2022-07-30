using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.OpenAPITools.Client;

namespace Data.Platform.Client.CSharp
{
    public class MTLS
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Configuration Configuration(string certPath = "certs/xecta-data-api.pem",
            string keyPath = "certs/xecta-data-api.key")
        {
            return new Configuration()
            {
                ClientCertificates = _x509CertificateCollection(certPath, keyPath)
            };
        }

        internal static X509CertificateCollection _x509CertificateCollection(string certPath = "certs/xecta-data-api.pem",
            string keyPath = "certs/xecta-data-api.key")
        {
            var cert = File.ReadAllBytes(certPath);
            var key = File.ReadAllText(keyPath);
            var certificate = new X509Certificate2(cert);

            using var rsa = RSA.Create();
            rsa.ImportFromPem(key);
            var x509 = certificate.CopyWithPrivateKey(rsa);

            return new X509CertificateCollection { x509 };
        }
    }
}