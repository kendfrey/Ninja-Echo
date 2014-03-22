using System;
using System.Threading.Tasks;

namespace NinjaEcho
{
    using Web;
    class Bot
    {
        private Client Client
        {
            get;
            set;
        }

        private string Fkey
        {
            get;
            set;
        }

        public async Task Start()
        {
            Client = new Client();
            Credentials credentials;
            try
            {
                credentials = Credentials.LoadFrom("credentials.txt");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("No credentials found.");
                return;
            }
            Console.WriteLine("Logging in to Stack Exchange...");
            await LoginToStackExchange(credentials);
            Console.WriteLine("Logging in to Stack Overflow...");
            await LoginToStackOverflow();
            Console.WriteLine("Entering chat...");
            await LoginToChat();
            Console.WriteLine("Ready");
            //await SendMessage("Hello, World!", "1");
        }

        private async Task LoginToStackExchange(Credentials credentials)
        {
            Response loginResponse = await Client.Request("https://openid.stackexchange.com/account/login/").Get();
            string fkey = System.Text.RegularExpressions.Regex.Match(loginResponse.Content, @"[\da-f-]{36}").Value;
            string form = string.Format("email={0}&password={1}&fkey={2}", Uri.EscapeDataString(credentials.Email), Uri.EscapeDataString(credentials.Password), fkey);
            await Client.Request("https://openid.stackexchange.com/account/login/submit/").Post(form, "application/x-www-form-urlencoded");
        }

        private async Task LoginToStackOverflow()
        {
            Response loginResponse = await Client.Request("http://stackoverflow.com/users/login/").Get();
            string fkey = System.Text.RegularExpressions.Regex.Match(loginResponse.Content, @"[\da-f]{32}").Value;
            string form = string.Format("openid_identifier={0}&fkey={1}", Uri.EscapeDataString("https://openid.stackexchange.com/"), fkey);
            await Client.Request("http://stackoverflow.com/users/authenticate/").Post(form, "application/x-www-form-urlencoded");
        }

        private async Task LoginToChat()
        {
            Response response = await Client.Request("http://chat.stackoverflow.com/").Get();
            Fkey = System.Text.RegularExpressions.Regex.Match(response.Content, @"""([\da-f]{32})""").Groups[1].Value;
        }

        private async Task SendMessage(string message, string room)
        {
            string form = string.Format("text={0}&fkey={1}", Uri.EscapeDataString(message), Fkey);
            Response response = await Client.Request(string.Format("http://chat.stackoverflow.com/chats/{0}/messages/new/", room)).Post(form, "application/x-www-form-urlencoded");
        }
    }
}
