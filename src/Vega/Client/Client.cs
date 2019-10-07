using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Vega.Base;

namespace Vega.Client
{
    public class Client : IClient
    {
        private readonly string _ip;
        private readonly int _port;
        private readonly TcpClient tcpClient;
        public Client(string ip,int port)
        {
            _ip = ip;
            _port = port;

            tcpClient = new TcpClient();
            tcpClient.Connect(_ip, port);
        }
        public Task<string> SendAsync(string message)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] messageBytes = encoder.GetBytes(message);

            
            using (var stream = tcpClient.GetStream())
            {

            }


            throw new NotImplementedException();
        }
    }
}
