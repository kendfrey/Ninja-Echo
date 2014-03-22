using System.Net;

namespace NinjaEcho.Web
{
    class Client
    {
        private CookieContainer Cookies
        {
            get;
            set;
        }

        public Client()
        {
            Cookies = new CookieContainer();
        }

        public Request Request(string url)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.CookieContainer = Cookies;
            return new Request(request);
        }
    }
}
