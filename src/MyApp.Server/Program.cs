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
            //var ip = IPAddress.Parse("127.100.100.50");
            //TcpListener connection = new TcpListener(ip, 3000);

            //connection.Start();

            //Console.WriteLine($"Server is Running [{connection.LocalEndpoint}]");

            //Console.WriteLine("Waiting for Connections...");

            //Socket s = connection.AcceptSocket();

            //Console.WriteLine("Connection Accepted From:" + s.RemoteEndPoint);

            //byte[] b = new byte[100];
            //int k = s.Receive(b);

            //Console.WriteLine("Recieved..");

            //for (int i = 0; i < k; i++)
            //{
            //    Console.Write(Convert.ToChar(b[i]));
            //}

            //var encode = new ASCIIEncoding();

            //s.Send(encode.GetBytes("Pong"));

            //Console.WriteLine("\n Pong Sent");

            //s.Close();
            //connection.Stop();

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var address = IPAddress.Parse("127.100.100.50");
            var port = 3000;

            var endpoint = new IPEndPoint(address, port);

            socket.Bind(endpoint);
            socket.Listen(10);
            Console.WriteLine("Starting...");
            while (true)
            {
                Console.WriteLine("Waiting connection ... ");

                Socket serverSocket = socket.Accept();

                byte[] bytes = new Byte[1024];
                string data = null;

                while (true)
                {
                    Console.WriteLine("\n Ping received. ");
                    int numByte = serverSocket.Receive(bytes);

                    data += Encoding.ASCII.GetString(bytes, 0, numByte);

                    break; // assuming message size is less than 1KB
                }

                Console.WriteLine("Request -> {0} ", data);
                byte[] message = Encoding.ASCII.GetBytes("Pong");
                await serverSocket.SendAsync(message, SocketFlags.None);
                Console.WriteLine("Response-> {0} ", message);

                serverSocket.Shutdown(SocketShutdown.Both);
                serverSocket.Close();
                break;
            }


        }
    }
}
