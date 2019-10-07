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
                // Data buffer 
                byte[] bytes = new Byte[1024];
                string data = null;

                while (true)
                {
                    Console.WriteLine("\n Ping received. ");
                    int numByte = serverSocket.Receive(bytes);

                    data += Encoding.ASCII.GetString(bytes,
                                               0, numByte);

                    //if (data.IndexOf("<EOF>") > -1)
                        break;
                }

                Console.WriteLine("Text received -> {0} ", data);
                byte[] message = Encoding.ASCII.GetBytes("Pong");

                // Send a message to Client  
                // using Send() method 
                serverSocket.Send(message);

                // Close client Socket using the 
                // Close() method. After closing, 
                // we can use the closed Socket  
                // for a new Client Connection 
                serverSocket.Shutdown(SocketShutdown.Both);
                serverSocket.Close();
                break;
            }

            ////await socket.ConnectAsync(endpoint);
            //if (socket.Connected)
            //{
            //    ASCIIEncoding encoder = new ASCIIEncoding();

            //    var response = new ArraySegment<byte>(new byte[512], 0, 512);
            //    await socket.ReceiveAsync(response, SocketFlags.None);

                
            //    byte[] messageBytes = encoder.GetBytes("Pong");

            //    await socket.SendAsync(messageBytes, SocketFlags.None);

            //    Console.WriteLine(encoder.GetString(response));
            //}

            //Console.ReadLine();
        }
    }
}
