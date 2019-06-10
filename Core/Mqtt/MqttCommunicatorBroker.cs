using System;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Server;

namespace Mqtt
{
    public class MqttCommunicatorBroker
    {
        private int port;
        private IMqttServer mqttServer;

        public MqttCommunicatorBroker(int port)
        {
            this.port = port;
        }

        public async Task<bool> IsRunning()
        {
            try
            {
                var client = new MqttFactory().CreateMqttClient();
                var clientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer("127.0.0.1", port)
                    .Build();
                var result = await client.ConnectAsync(clientOptions);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task StartAsync()
        {
            try
            {
                mqttServer = new MqttFactory().CreateMqttServer();

                var optionsBuilder = new MqttServerOptionsBuilder()
                    .WithDefaultEndpointPort(port);

                mqttServer.UseClientConnectedHandler(OnClientConnected);
                mqttServer.UseClientDisconnectedHandler(ClientDisconnected);
                await mqttServer.StartAsync(optionsBuilder.Build());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to start the server: " + ex);
                throw;
            }
        }

        private async Task ClientDisconnected(MqttServerClientDisconnectedEventArgs arg)
        {
            System.Diagnostics.Debug.WriteLine($"Client {arg.ClientId} disconnected");
        }

        async Task OnClientConnected(MqttServerClientConnectedEventArgs eventArgs)
        {
            System.Diagnostics.Debug.WriteLine("New Client connected with client id: " + eventArgs.ClientId);
        }
    }
}
