using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Vega
{
    public class Server
    {
        private readonly string _ip;
        private readonly int _port;

        private Socket socket;
        private Socket serverSocket;

        public Server(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        public async Task Start()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var address = IPAddress.Parse(_ip);
            var endpoint = new IPEndPoint(address, _port);

            socket.Bind(endpoint);
            socket.Listen(10);

            while (true)
            {
                Console.WriteLine("Waiting connection ... ");

                serverSocket = socket.Accept();

                byte[] requestBytes = new Byte[1024];
                string requestMessage = null;

                while (true)
                {
                    
                    int requestLength = serverSocket.Receive(requestBytes);

                    requestMessage += Encoding.ASCII.GetString(requestBytes, 0, requestLength);
                    Console.WriteLine($"\n {requestMessage} received. ");

                    break; // assuming message size is less than 1KB
                }

                byte[] message = Encoding.ASCII.GetBytes("Pong");
                await serverSocket.SendAsync(message, SocketFlags.None);

            }
        }

        public void Shutdown()
        {
            serverSocket.Shutdown(SocketShutdown.Both);
            serverSocket.Close();
        }
    }
}
