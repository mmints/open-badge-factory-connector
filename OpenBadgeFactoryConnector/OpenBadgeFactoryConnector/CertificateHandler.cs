using System;
using System.Security.Cryptography.X509Certificates;

namespace OpenBadgeFactoryConnector
{
    public class CertificateHandler
    {
        public static X509Certificate2 GetCertificate2FromPersonalKeyStoreByClientId(string client_id)
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            try
            {
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certificates = store.Certificates;
                foreach (X509Certificate2 cert in certificates)
                {
                    if (cert.SubjectName.Name == "CN=" + client_id)
                    {
                        return cert;
                    }
                    else
                    {
                        throw new Exception("No certificate with subject name [" + "CN=" + client_id +
                                            "] has been found! Make sure that its installed correctly!");
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Loading personal key store on local machine failed: " + exception.Message);
            }
            finally
            {
                store.Close();
            }
            return null;
        }
    }
}