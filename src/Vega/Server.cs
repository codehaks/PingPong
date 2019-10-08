﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vega
{
    public class Server
    {
        private readonly string _ip;
        private readonly int _port;

        private Socket listner;
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public Server(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        public async Task Start()
        {
            listner = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var address = IPAddress.Any;//.Parse(_ip);
            var endpoint = new IPEndPoint(address, _port);

         
            listner.Bind(endpoint);
            listner.Listen(100);



            while (true)
            {
                allDone.Reset();
                Console.WriteLine("Waiting connection ... ");

                var handlerTask =  listner.AcceptAsync();

                Console.WriteLine("Connecting ... ");

                listner.BeginAccept(
                   new AsyncCallback(AcceptCallback),
                   listner);

                // Wait until a connection is made before continuing.  
                allDone.WaitOne();

                //Task.Run(() => HandleConnection(handlerTask));

            }



        }

        // State object for reading client data asynchronously  
        public class StateObject
        {
            // Client  socket.  
            public Socket workSocket = null;
            // Size of receive buffer.  
            public const int BufferSize = 1024;
            // Receive buffer.  
            public byte[] buffer = new byte[BufferSize];
            // Received data string.  
            public StringBuilder sb = new StringBuilder();
        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.  
            StateObject state = new StateObject();

            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket.   
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read   
                // more data.  
                content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    // All the data has been read from the   
                    // client. Display it on the console.  
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);
                    // Echo the data back to the client.  
                    Send(handler, content);
                }
                else
                {
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }

        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        //public static int Main(String[] args)
        //{
        //    StartListening();
        //    return 0;
        //}

        public async Task HandleConnection(Task<Socket> handlerTask)
        {
            var handler = await handlerTask;
            
            Console.WriteLine("Connected ... ");
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
            //handler.Shutdown(SocketShutdown.);
            //handler.Close();
        }

        public void Shutdown()
        {
            listner.Shutdown(SocketShutdown.Both);
            listner.Close();
        }
    }
}
