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
        private static MqttCommunicatorClient mqttClient;
        private static CodeChangeHandler codeChangeHandler;

        private static List<LogEvent> queuedLogs = new List<LogEvent>();

        public static ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();

        static Initializer()
        {
            mqttBroker = new MqttCommunicatorBroker(Constants.DEFAULT_PORT);
            mqttClient = new MqttCommunicatorClient("127.0.0.1", Constants.DEFAULT_PORT);

            mqttClient.Subscribe(Topics.PING);
            mqttClient.Subscribe(HotReloading.Core.Topics.Log);

            mqttClient.MessageReceived += MqttMessageHandler.HandleMessage;

            MqttMessageHandler.ClientConnected += (client) => Clients.Add(client);
            MqttMessageHandler.ReceivedLog += (clientId, log) => LogsViewModel.Logs.Add(new KeyValuePair<string, LogEvent>(clientId, log));
            Logger.Current.Logged += Log;

            //mqttClient.Subscribe("Log/+");
        }

        static async void Log(LogEvent log)
        {
            if (mqttClient.IsConnected)
                await mqttClient.Publish(Serializer.SerializeJson(log), HotReloading.Core.Topics.Log);
            else
                queuedLogs.Add(log);
        }


        public static async Task Init(IIde ide, string ideName)
        {
            codeChangeHandler = new CodeChangeHandler(ide);
            if (!await mqttBroker.IsRunning())
                await mqttBroker.StartAsync();

            await mqttClient.ConnectAsync(ideName);

            foreach (var log in queuedLogs)
                await mqttClient.Publish(Serializer.SerializeJson(log), HotReloading.Core.Topics.Log);
            //await mqttClient.Publish("Log/1", "Test Data");
        }
    }
}
