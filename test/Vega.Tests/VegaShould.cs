using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
#pragma warning disable CS4014 
namespace Vega.Tests
{
    public class VegaShould
    {

        [Fact]
        public async Task ReturnPongs()
        {
            var port = 3000;
            var ip = "127.100.100.50";
            var server = new Vega.Server(ip, port);

            Task.Run(() =>
            {

                server.Start();
            });

            await Task.Delay(3000);


            var client = new Vega.Client(ip, port);
            //await client.Connect();

            var r1 = await client.SendAsync("Ping");

            //await client.Connect();
            var r2 = await client.SendAsync("Ping");

            Assert.StartsWith("Pong", r1);
            Assert.StartsWith("Pong", r2);
        }

        [Fact]
        public async Task ReturnPongForPing()
        {
            var port = 3000;
            var ip = "127.100.100.50";
            var server = new Vega.Server(ip, port);

            Task.Run(() =>
           {

               server.Start();
           });

            await Task.Delay(3000);


            var client = new Vega.Client(ip, port);
            //await client.Connect();

            var response = await client.SendAsync("Ping");
            //server.Shutdown();
            //client.Close();

            Assert.StartsWith("Pong",response);
        }

        [Fact]
        public async Task ReturnMultiplePongs()
        {
            var port = 3000;
            var ip = "127.100.100.50";
            var server = new Vega.Server(ip, port);


            Task.Run(() =>
            {
                server.Start();
            });


            await Task.Delay(3000);


            var client = new Vega.Client(ip, port);
            //await client.Connect();

            var responseList = new List<Task<string>>();

            var numberOfCalls = 3;
            for (int i = 0; i < numberOfCalls; i++)
            {
                //await client.Connect();
                var task = client.SendAsync("Ping");

                responseList.Add(task);
                
            }

            var result=await Task.WhenAll(responseList);

            Assert.True(result.Length == numberOfCalls);

        }
    }
}


