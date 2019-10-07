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

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var address = IPAddress.Parse("127.100.100.50");
            var port = 3000;

            var endpoint = new IPEndPoint(address, port);

            Console.WriteLine("Connecting...");
            await socket.ConnectAsync(endpoint);
            if (socket.Connected)
            {
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] messageBytes = encoder.GetBytes("Ping");

                await socket.SendAsync(messageBytes,SocketFlags.None);
                Console.WriteLine("\n Ping sent. ");
                var response= new ArraySegment<byte>(new byte[512], 0, 512);

                await socket.ReceiveAsync(response, SocketFlags.None);

                Console.WriteLine(encoder.GetString(response));
            }

            
        }
    }
}
