using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace OpenBadgeFactoryConnector
{
    
    /// <summary>
    /// Cass <c>API</c>
    /// Generic certificate based API class for communication with a RESTful Web-API.
    /// </summary>
    public class API
    {
        private string url;
        private string client_id;

        private HttpClientHandler client_handler = new HttpClientHandler();
        private HttpClient client;

        /// <summary>
        /// Constructor <c>API</c>
        /// </summary>
        /// <param name="url">URL to the API</param>
        /// <param name="certificate">Client certificate for SSL/TSL communication with the web server</param>
        /// <param name="client_id">Client ID of the requester</param>
        public API(string url, X509Certificate2 certificate, string client_id)
        {
            this.url = url;
            this.client_handler.ClientCertificates.Add(certificate);
            this.client = new HttpClient(this.client_handler);
            this.client_id = client_id;
        }

        /// <summary>
        /// This function provides a request towards the API's web service and return the response body as a string.
        /// </summary>
        /// <param name="endpoint">Requested endpoint</param>
        /// <returns>Response Body</returns>
        /// <example>string all_badges = api.GetRequest("badge");</example>
        public async Task<string> GetRequest(string endpoint)
        {
            var responseBody = "";

            try
            {
                responseBody = await client.GetStringAsync(this.url + "/" + endpoint + "/" + this.client_id);
                
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                Console.WriteLine("Inner Exception :{0} ", e.InnerException);
            }

            // Need to call dispose on the HttpClient and HttpClientHandler objects 
            // when done using them, so the app doesn't leak resources
            client_handler.Dispose();
            client.Dispose();
            return responseBody;
        }
    }
}