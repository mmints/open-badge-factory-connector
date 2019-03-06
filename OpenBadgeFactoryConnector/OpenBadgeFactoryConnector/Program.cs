using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace OpenBadgeFactoryConnector
{
    class Program
    {        
        static async Task Main()
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
                string responseBody = await client.GetStringAsync("https://openbadgefactory.com/v1/earnablebadge/NM70OHe7HCeO");

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
    }
}
