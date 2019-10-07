using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Server
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var server = new Vega.Server("127.100.100.50", 3000);
            await server.Start();
           
        }
    }
}
