using System;
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
            string Certificate = "/Users/mark/Developer/open-badge-factory-connector-cs/OpenBadgeFactoryConnector/OpenBadgeFactoryConnector/certificates/certificate.pem";
            
            // Load the certificate into an X509Certificate object.
            X509Certificate cert = new X509Certificate(Certificate);
            
            string resultsTrue = cert.ToString(true);
            Console.WriteLine(resultsTrue);
            
            string resultsFalse = cert.ToString(false);
            Console.WriteLine(resultsFalse);
            
            // Create an HttpClientHandler object and set to use default credentials
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            // Create an HttpClient object
            HttpClient client = new HttpClient(handler);
            
            // Call asynchronous network methods in a try/catch block to handle exceptions
            try	
            {
                string responseBody = await client.GetStringAsync("https://openbadgefactory.com/v1/ping/NM70OHe7HCeO");

                Console.WriteLine(responseBody);
            }  
            catch(HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");	
                Console.WriteLine("Message :{0} ",e.Message);
            }

            // Need to call dispose on the HttpClient and HttpClientHandler objects 
            // when done using them, so the app doesn't leak resources
            handler.Dispose();
            client.Dispose();
        }
    }
}
