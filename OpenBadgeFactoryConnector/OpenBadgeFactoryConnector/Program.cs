using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Text;

namespace OpenBadgeFactoryConnector
{
    class Program
    {
        public static  async Task test()
        {
            Connector con = new Connector("NM70OHe7HCeO");
            var badges = await con.GetAllBadges();
            
            Console.WriteLine(badges[0].name);
            
        }

        static async Task Main()
        {
            await test(); 
        }

    }
}
