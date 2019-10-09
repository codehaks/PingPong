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

            Vega.Client.Start("127.100.100.50", 3000);
            await Vega.Client.SendAsync("Ping");
            await Vega.Client.SendAsync("Ping");
            await Vega.Client.SendAsync("Ping");
            Vega.Client.Close();
            
           
        }
    }
}
