using System.IO;
using System.Net;

namespace NinjaEcho.Web
{
    class Response
    {
        private HttpWebResponse HttpResponse
        {
            get;
            set;
        }

        public string Content
        {
            get;
            private set;
        }

        public Response(HttpWebResponse response)
        {
            HttpResponse = response;
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                Content = reader.ReadToEnd();
            }
        }
    }
}
