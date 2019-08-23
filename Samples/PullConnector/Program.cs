using System;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.TimeSeries.Runtime.Connectors.Grpc;
using Grpc.Core;
using Microsoft.Extensions.Hosting;

namespace PullConnector
{
    class Program
    {
        static void HandleStreams(AsyncDuplexStreamingCall<PullConnectorGetData, PullConnectorData> stream)
        {
            Task.Run(async() =>
            {
                Console.WriteLine("Wait for messages");
                while (await stream.ResponseStream.MoveNext(CancellationToken.None))
                {
                    Console.WriteLine("Message received on client");
                    
                }
            });

            Task.Run(async() =>
            {
                for( ;; )
                {
                    Console.WriteLine("Write message to server");
                    await stream.RequestStream.WriteAsync(new Dolittle.TimeSeries.Runtime.Connectors.Grpc.PullConnectorGetData());
                    //await stream.RequestStream.CompleteAsync();
                    await Task.Delay(1000);
                }

            });


        }


        static async Task Main(string[] args)
        {
            Console.WriteLine("Connect");
            var channel = new Channel("0.0.0.0", 50053, ChannelCredentials.Insecure);
            var client = new Dolittle.TimeSeries.Runtime.Connectors.Grpc.PullConnectors.PullConnectorsClient(channel);
            var metadata = new Metadata
            { { "clientId", Guid.NewGuid().ToString() }
            };

            Console.WriteLine("Register");
            var stream = client.Register(metadata);
            HandleStreams(stream);

            var hostBuilder = new HostBuilder();
            var host = hostBuilder.Build();

            await host.RunAsync();

        }
    }
}