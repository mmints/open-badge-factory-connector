using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Text;

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
            //string Certificate = "C:\\Users\\Mark\\IWM\\obf-certificates\\cert.pfx";
            string Certificate = "certificates/cert.pfx";

            X509Certificate2 cert = new X509Certificate2(Certificate, "odl4u");
            Api api = new Api("https://openbadgefactory.com/v1", cert, "NM70OHe7HCeO");
            
            string response = await api.GetRequest("badge");
            Console.WriteLine(response);
            // File.WriteAllText("C:\\Users\\Mark\\IWM\\test.json", response);
            File.WriteAllText("response.json", response);
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

        public static async Task test_response_class()
        {
            Connector connector = new Connector("NM70OHe7HCeO");
            Response res = await connector.GetAllBadges();
            res.WriteResponseBodyToJson("response.json");
            string[] response_lines = res.ReadAllLineResponseBodyJson("response.json");
            Console.WriteLine(response_lines[0]);
            Console.WriteLine("\n" + "###################################################################" + "\n" + "###################################################################" +"\n" + "###################################################################");
            string temp = response_lines[0].Replace("\\n", "\n");
            Console.WriteLine(temp);
        }
        

        static async Task Main()
        {
            await test_response_class(); 
        }

    }
}
