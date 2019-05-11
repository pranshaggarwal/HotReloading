using System;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;

namespace MqttClient.NetCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Client");
            var factory = new MqttFactory();
            var client = factory.CreateMqttClient();
            client.UseConnectedHandler(async (arg) =>
            {
                Console.WriteLine("Connected with server");
                await client.SubscribeAsync(new MqttClientSubscribeOptions()
                {
                    TopicFilters = new System.Collections.Generic.List<MQTTnet.TopicFilter>
                    {
                        new TopicFilterBuilder().WithTopic("HotReloading").WithExactlyOnceQoS().Build()
                    }
                }, CancellationToken.None);
            });
            client.UseApplicationMessageReceivedHandler((arg) =>
            {
                Console.WriteLine("Data Received: " + arg.ApplicationMessage.ConvertPayloadToString());
            });

            var clientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer("192.168.1.155", 8076)
                .Build();

            await client.ConnectAsync(clientOptions);

            Console.WriteLine("Press any key to publish test data");

            Console.ReadLine();

            var message = new MqttApplicationMessageBuilder()
                .WithTopic("HotReloading")
                .WithExactlyOnceQoS()
                .WithPayload("Test data")
                .Build();
            await client.PublishAsync(message);

            Console.WriteLine("Press any key to close app");

            Console.ReadLine();
            await client.DisconnectAsync();
        }
    }
}
