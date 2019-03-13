using System.IO;

namespace OpenBadgeFactoryConnector
{
    public class Response
    {
        private string _responseBody { get;}

        public Response(string responseBody)
        {
            _responseBody = responseBody;
        }
        
        public void WriteResponseBodyToJson(string path)
        {
            File.WriteAllText(path, _responseBody);
        }

        public string[] ReadAllLineResponseBodyJson(string path)
        {
            var responseBodyLines = File.ReadAllLines(path);
            return responseBodyLines;
        }
        
        ~Response()
        {
            System.Diagnostics.Trace.WriteLine("Response's destructor is called.");
        }
    }
}