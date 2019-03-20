using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private readonly Adapter _adapter;

        /// <summary>
        /// Constructor <c>Connector</c>
        /// </summary>
        /// <param name="clientId">Client ID of the requester</param>
        public Connector(string clientId)
        {
            X509Certificate2 certificate = new X509Certificate2("certificates/cert.pfx", "odl4u"); // Just for testing
            _adapter = new Adapter("https://openbadgefactory.com/v1", certificate, clientId);
        }

        /// <summary>
        /// Deserialize all badges available and save them into a list.  
        /// </summary>
        /// <returns>A list containing all badges from OpenBadgeFactory.</returns>
        public async Task<List<Badge>> GetAllBadges()
        {    
            List<string> responseBodyLines = await GetAllBadgesRequest();
            List<Badge> allBadges = new List<Badge>();

            foreach (var response in responseBodyLines)
            {
                try
                {
                    Badge badge = Badge.CreatFromJson(response);
                    allBadges.Add(badge);                 
                }
                catch (Exception e)
                {
                    Console.WriteLine("It was not possible to deserialize the badge. Message: " + e.Message);
                   //throw;
                }
            }
            return allBadges;
        }
        
        /// <summary>
        /// This function returns a JSON string list that includes all accessible badges.
        /// </summary>
        /// <returns>response body (json) as string list separated by lines which includes all badges.</returns>
        public async Task<List<string>> GetAllBadgesRequest()
        {
            string responseBody = await _adapter.GetRequest("badge");
            string[] responseBodyLines = responseBody.Split("\n");
            List<string> responseBodyList = responseBodyLines.ToList();
            removeEmptyStingsFromResponseBody(responseBodyList);
            return responseBodyList;     
        }

        // Helping functions to deal with the responses better
        private static void removeEmptyStingsFromResponseBody(List<string> responseBody)
        {
            var emptyEntry = "";
            responseBody.Remove(emptyEntry);

        }

    }
}