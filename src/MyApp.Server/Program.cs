using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Server
{
    class Program
    {
        static void Main(string[] args)
        {

            Vega.Server.Start("127.100.100.50", 3000);

            //Vega.Server(.Start("127.100.100.50", 3000);
            //await server.Start();
           
        }
    }
}
