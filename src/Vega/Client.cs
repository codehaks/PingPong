using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Vega
{
    public class Client : IClient
    {
        private readonly string _ip;
        private readonly int _port;
        private Socket socket;
        public Client(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        private async Task Connect()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var address = IPAddress.Parse(_ip);
            var endpoint = new IPEndPoint(address, _port);
            await socket.ConnectAsync(endpoint);
        }
        public async Task<string> SendAsync(string message)
        {
            await Connect();

            if (socket.Connected == false)
            {
                throw new Exception("Not connected!");

            }
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] messageBytes = encoder.GetBytes(message);


            await socket.SendAsync(messageBytes, SocketFlags.None);
            var response = new ArraySegment<byte>(new byte[1024], 0, 1024);

            await socket.ReceiveAsync(response, SocketFlags.None);

            return encoder.GetString(response).Trim();
        }

        public void Close()
        {
            socket.Close();
        }
    }
}
