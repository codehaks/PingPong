using System;
using System.Threading.Tasks;
using Xunit;

namespace Vega.Tests
{
    public class VegaShould
    {
        [Fact]
        public async Task ReturnPongForPing()
        {
            var port = 3000;
            var ip = "127.100.100.50";
            var server = new Vega.Server(ip, port);

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(() =>
           {

               server.Start();
           });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            await Task.Delay(3000);


            var client = new Vega.Client(ip, port);
            await client.Connect();

            var response = await client.SendAsync("Ping");
            //server.Shutdown();
            //client.Close();

            Assert.StartsWith("Pong",response);




        }
    }
}
