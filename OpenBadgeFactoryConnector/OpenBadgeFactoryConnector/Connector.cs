using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;


namespace OpenBadgeFactoryConnector
{
    /// <summary>
    /// Class<c>Connector</c>
    /// Top level communication connector to open badge factory's API.
    /// </summary>
    public class Connector
    {
        private readonly Api _api;

        /// <summary>
        /// Constructor <c>Connector</c>
        /// </summary>
        /// <param name="clientId">Client ID of the requester</param>
        public Connector(string clientId)
        {
            X509Certificate2 certificate = new X509Certificate2("certificates/cert.pfx", "odl4u"); // Just for testing
            _api = new Api("https://openbadgefactory.com/v1", certificate, clientId);
        }

        /// <summary>
        /// This function returns all accessible badges.
        /// </summary>
        /// <returns>response bode (json) which includes all badges.</returns>
        public async Task<Response> GetAllBadges()
        {
            var responseBody = await _api.GetRequest("badge");
            Response response = new Response(responseBody);
            return response;
            
        }
    }
}