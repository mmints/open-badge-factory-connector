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
        private string client_id;
        private API api;
        private X509Certificate2 certificate; 
        
        /// <summary>
        /// Constructor <c>Connector</c>
        /// </summary>
        /// <param name="client_id">Client ID of the requester</param>
        public Connector(string client_id)
        {
            this.certificate = new X509Certificate2("C:\\Users\\Mark\\IWM\\obf-certificates\\cert.pfx", "odl4u"); // Just for testing 
            this.client_id = client_id;
            this.api = new API("https://openbadgefactory.com/v1", this.certificate, this.client_id);
        }

        /// <summary>
        /// This function returns all accessible badges.
        /// </summary>
        /// <returns>response bode (json) which includes all badges.</returns>
        public async Task<string> GetAllBadges()
        {
            string responseBody = await this.api.GetRequest("badge");
            return responseBody;
        }
    }
}