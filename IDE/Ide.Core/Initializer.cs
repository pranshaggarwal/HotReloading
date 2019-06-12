using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HotReloading.Core;
using Ide.Core.Mqtt;
using Ide.Core.ViewModel;
using Log;
using Mqtt;
using Topics = Mqtt.Topics;

namespace Ide.Core
{ 
    public static class Initializer
    {
        private static MqttCommunicatorBroker mqttBroker;
        public static MqttCommunicatorClient mqttClient;
        private static CodeChangeHandler codeChangeHandler;

        public static ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();

        static Initializer()
        {
            mqttBroker = new MqttCommunicatorBroker(Constants.DEFAULT_PORT);
            mqttClient = new MqttCommunicatorClient("127.0.0.1", Constants.DEFAULT_PORT);

            mqttClient.Subscribe(Topics.PING);
            mqttClient.Subscribe(Topics.Log);

            mqttClient.MessageReceived += MqttMessageHandler.HandleMessage;

            MqttMessageHandler.ClientConnected += Client_Connected;
            MqttMessageHandler.ReceivedLog += (clientId, log) => LogsViewModel.Logs.Add(new KeyValuePair<string, LogEvent>(clientId, log));
        }

        private static async void Client_Connected(Client client)
        {
            Clients.Add(client);
            await mqttClient.Publish("", Topics.PING_RESPONSE.Replace("+", client.Id));
        }

        public static async Task Init(IIde ide, string ideName)
        {
            codeChangeHandler = new CodeChangeHandler(ide);
            if (!await mqttBroker.IsRunning())
                await mqttBroker.StartAsync();

            await mqttClient.ConnectAsync(ideName);
        }
    }
}
