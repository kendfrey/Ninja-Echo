using System.Threading.Tasks;

namespace NinjaEcho
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();
            Task startTask = bot.Start();
            startTask.Wait();
        }
    }
}
