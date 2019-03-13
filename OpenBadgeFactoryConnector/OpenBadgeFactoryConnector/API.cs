using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace OpenBadgeFactoryConnector
{
    /// <summary>
    /// Class <c>API</c>
    /// Generic certificate based API class for communication with a RESTful Web-API.
    /// </summary>
    public class Api
    {
        private readonly string _url;
        private readonly string _clientId;

        private readonly HttpClientHandler _clientHandler = new HttpClientHandler();
        private readonly HttpClient _client;

        /// <summary>
        /// Constructor <c>API</c>
        /// </summary>
        /// <param name="url">URL to the API</param>
        /// <param name="certificate">Client certificate for SSL/TSL communication with the web server</param>
        /// <param name="clientId">Client ID of the requester</param>
        public Api(string url, X509Certificate2 certificate, string clientId)
        {
            _url = url;
            _clientHandler.ClientCertificates.Add(certificate);
            _client = new HttpClient(this._clientHandler);
            _clientId = clientId;
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
                responseBody = await _client.GetStringAsync(_url + "/" + endpoint + "/" + _clientId);   
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                Console.WriteLine("Inner Exception :{0} ", e.InnerException);
            }

            // Need to call dispose on the HttpClient and HttpClientHandler objects 
            // when done using them, so the app doesn't leak resources
            _clientHandler.Dispose();
            _client.Dispose();
            return responseBody;
        }
    }
}