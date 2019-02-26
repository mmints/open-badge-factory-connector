﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenBadgeFactoryConnector
{
    class Program
    {        
        static async Task Main()
        {
            // Create a New HttpClient object and dispose it when done, so the app doesn't leak resources
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                try	
                {
                    HttpResponseMessage response = await client.GetAsync("https://openbadgefactory.com/v1/ping/NM70OHe7HCeO");
                    // response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Above three lines can be replaced with new helper method below
                    // string responseBody = await client.GetStringAsync(uri);

                    Console.WriteLine(responseBody);
                }  
                catch(HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");	
                    Console.WriteLine("Message :{0} ",e.Message);
                }
            }
        }
    }
}
