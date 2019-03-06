using System;

namespace OpenBadgeFactoryConnector
{
    public class Connector
    {
        private string client_id;
        private API api;
        public Connector(string client_id, string certificate_file)
        {
            this.client_id = client_id;
            this.api = new API("https://openbadgefactory.com/v1", certificate_file); // The entered URL is given by open badge factory
        }
    }
}