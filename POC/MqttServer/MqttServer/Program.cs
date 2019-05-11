using System;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Server;

namespace MqttServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Server!");
            var mqttServer = new MqttFactory().CreateMqttServer();

            var optionsBuilder = new MqttServerOptionsBuilder()
                .WithDefaultEndpointPort(8076);

            mqttServer.ClientConnectedHandler = new MqttServerClientConnectedHandlerDelegate((arg) =>
            {
                Console.WriteLine("New Client Connected with clientId: " + arg.ClientId);
            });

            mqttServer.ClientSubscribedTopicHandler = new MqttServerClientSubscribedHandlerDelegate((arg) =>
            {
                Console.WriteLine("Client Subscribed Topic: " + arg.TopicFilter.Topic);
            });

            await mqttServer.StartAsync(optionsBuilder.Build());

            Console.ReadLine();

            await mqttServer.StopAsync();
        }
    }
}
