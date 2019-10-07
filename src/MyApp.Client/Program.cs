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
            await client.Connect();

            while (true)
            {
                Console.WriteLine("Message : ");
                var request = Console.ReadLine();
                var response = await client.SendAsync(request);
                Console.WriteLine(response.Trim());
            }

                

        }
    }
}
