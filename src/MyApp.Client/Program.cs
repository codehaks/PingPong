using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var client = new Vega.Client("127.100.100.50", 3000);
            //await client.Connect();


            for (int i = 0; i < 5; i++)
            {
                var response = await client.SendAsync($"Ping({i})");
                Console.WriteLine(response.Trim());
            }

        }
    }
}
