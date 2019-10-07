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

        private Socket listner;
        private Socket handler;

        public Server(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        public async Task Start()
        {
            listner = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var address = IPAddress.Parse(_ip);
            var endpoint = new IPEndPoint(address, _port);

            listner.Bind(endpoint);
            listner.Listen(10);

            while (true)
            {
                Console.WriteLine("Waiting connection ... ");

                handler = listner.Accept();

                byte[] requestBytes = new Byte[1024];
                string requestMessage = null;

                while (true)
                {
                    
                    int requestLength = handler.Receive(requestBytes);

                    requestMessage += Encoding.ASCII.GetString(requestBytes, 0, requestLength);
                    Console.WriteLine($"\n {requestMessage} received. ");

                    break; // assuming message size is less than 1KB
                }

                byte[] message = Encoding.ASCII.GetBytes("Pong");
                await handler.SendAsync(message, SocketFlags.None);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
        }

        public void Shutdown()
        {
            listner.Shutdown(SocketShutdown.Both);
            listner.Close();
        }
    }
}
