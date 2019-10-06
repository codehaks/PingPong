using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MyApp.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var ip = IPAddress.Parse("127.100.100.50");
            TcpListener connection = new TcpListener(ip, 3000);

            connection.Start();

            Console.WriteLine($"Server is Running [{connection.LocalEndpoint}]");
            
            Console.WriteLine("Waiting for Connections...");

            Socket s = connection.AcceptSocket();

            Console.WriteLine("Connection Accepted From:" + s.RemoteEndPoint);

            byte[] b = new byte[100];
            int k = s.Receive(b);

            Console.WriteLine("Recieved..");

            for (int i = 0; i < k; i++)
            {
                Console.Write(Convert.ToChar(b[i]));
            }

            var encode = new ASCIIEncoding();

            s.Send(encode.GetBytes("Pong"));

            Console.WriteLine("\n Pong Sent");

            s.Close();
            connection.Stop();

            Console.ReadLine();
        }
    }
}
