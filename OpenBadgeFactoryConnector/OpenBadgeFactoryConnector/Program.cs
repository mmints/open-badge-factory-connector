using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace OpenBadgeFactoryConnector
{
    class Program
    {
        public static async Task test_1()
        {
            
            // The path to the certificate.
            string Certificate = "C:\\Users\\Mark\\IWM\\obf-certificates\\cert.pfx";

            
            string pwd= "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pwd += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pwd.Length > 0)
                    {
                        pwd = pwd.Substring(0, (pwd.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if(key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);
            
            // Load the certificate into an X509Certificate object.
            X509Certificate2 cert = new X509Certificate2(Certificate, pwd);
                
            string resultsTrue = cert.ToString(true);
            Console.WriteLine(resultsTrue);
            
            // Create an HttpClientHandler object and set to use default credentials
            HttpClientHandler handler = new HttpClientHandler();
            handler.ClientCertificates.Add(cert);

            // WebRequestHandler requestHandler = new WebRequestHandler(); // Only available on windows 
            
            // Create an HttpClient object
            HttpClient client = new HttpClient(handler);

            // Call asynchronous network methods in a try/catch block to handle exceptions
            try	
            {
                string responseBody = await client.GetStringAsync("https://openbadgefactory.com/v1/badge/NM70OHe7HCeO");

                File.WriteAllText("C:\\Users\\Mark\\IWM\\test.json", responseBody);
                Console.WriteLine(responseBody);
            }  
            catch(HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");	
                Console.WriteLine("Message :{0} ",e.Message);
                Console.WriteLine("Inner Exception :{0} ",e.InnerException);
            }

            // Need to call dispose on the HttpClient and HttpClientHandler objects 
            // when done using them, so the app doesn't leak resources
            handler.Dispose();
            client.Dispose();            
        }

        public static async Task test_2()
        {
            string Certificate = "C:\\Users\\Mark\\IWM\\obf-certificates\\cert.pfx";
            X509Certificate2 cert = new X509Certificate2(Certificate, "odl4u");
            API api = new API("https://openbadgefactory.com/v1", cert, "NM70OHe7HCeO");
            
            string response = await api.GetRequest("badge");
            Console.WriteLine(response);
            File.WriteAllText("C:\\Users\\Mark\\IWM\\test.json", response);


        }
        
        public static async Task test_3()
        {
            Connector obf = new Connector("NM70OHe7HCeO");
            await obf.GetAllBadges();
        }
        
        public static void test_4()
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);

            try
            {
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certificates = store.Certificates;
                foreach (X509Certificate2 cert in certificates)
                {
                    if (cert.SubjectName.Name == "CN=NM70OHe7HCeO")
                    {
                        Console.WriteLine(cert.GetExpirationDateString());
                        Console.WriteLine(cert.Issuer);
                        Console.WriteLine(cert.GetEffectiveDateString());
                        Console.WriteLine(cert.GetNameInfo(X509NameType.SimpleName, true));
                        Console.WriteLine(cert.HasPrivateKey);
                        Console.WriteLine(cert.SubjectName.Name);
                        Console.WriteLine("-----------------------------------");                        
                    }
                }
            }
            finally
            {
                store.Close();
            }
        }

        public static void test_5()
        {
            CertificateHandler ch = new CertificateHandler();
            try
            {
                X509Certificate2 cert = ch.GetCertificate2FromPersonalKeyStoreByClientId("NM70OHe7HCeO");
                Console.WriteLine(cert.GetExpirationDateString());
                Console.WriteLine(cert.Issuer);
                Console.WriteLine(cert.GetEffectiveDateString());
                Console.WriteLine(cert.GetNameInfo(X509NameType.SimpleName, true));
                Console.WriteLine(cert.HasPrivateKey);
                Console.WriteLine(cert.SubjectName.Name);
                Console.WriteLine("-----------------------------------");                        
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

       

        static async Task Main()
        {
            //Console.WriteLine("data:image/png;base64,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\n5I782vL/HWlvZ31Jd+MAAAAASUVORK5CYII=\n");
            await test_2(); 
        }
    }
}
