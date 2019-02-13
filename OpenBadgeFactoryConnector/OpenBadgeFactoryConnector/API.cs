using System;

namespace OpenBadgeFactoryConnector
{
    public class API
    {
        private string url;
        private string certificate_file;
        private string key_file;

        public API(string url, string certificate_file, string key_file)
        {
            this.url = url;
            this.certificate_file = certificate_file;
            this.key_file = key_file;
        }
    }
}