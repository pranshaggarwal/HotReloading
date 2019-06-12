using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Log;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;

namespace Mqtt
{
    public class MqttCommunicatorClient
    {
        private readonly string server;
        private readonly int port;
        private IMqttClient client;

        private List<string> topicsToSubcribe;
        private List<string> subscribedTopics;
        private DebugListener debugListener;
        private bool pingSuccess;
        private static List<LogEvent> queuedLogs = new List<LogEvent>();

        public event Action<MqttApplicationMessageReceivedEventArgs> MessageReceived;

        public bool IsConnected => client.IsConnected;

        public string ClientId => client.Options.ClientId;

        public MqttCommunicatorClient(string server, int port)
        {
            this.server = server;
            this.port = port;
            topicsToSubcribe = new List<string>();
            subscribedTopics = new List<string>();
            client = new MqttFactory().CreateMqttClient();
            client.UseApplicationMessageReceivedHandler(OnMessageReceived);
            client.UseDisconnectedHandler(OnDisconnected);

            Subscribe(Topics.PING_RESPONSE);

            Logger.Current.Logged += Log;

            debugListener = new DebugListener(this);
            System.Diagnostics.Trace.Listeners.Add(debugListener);
        }

        public async Task ConnectAsync(string clientName)
        {
            try
            {
                var clientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer(server, port)
                    .Build();
                var result = await client.ConnectAsync(clientOptions);
                if (result.ResultCode == MqttClientConnectResultCode.Success)
                {
                    await SubscribePendingTopics();
                    await Ping(clientName);
                    await PublishQueuedLogs();
                    Logger.Current.WriteInfo("Connected to Mqtt Broker with client id: " + client.Options.ClientId);
                }
                //else
                //{
                //    var message = "Cannot connect to MQTT server. Response: " + result;
                //    Logger.Current.WriteError(message);
                //}
            }
            catch (Exception ex)
            {
                //Logger.Current.WriteError("Cannot connect to MQTT server. Exception: " + ex);
                //throw;
            }
        }

        private async Task Ping(string clientName)
        {
            while (!pingSuccess)
            {
                debugListener.Pause();
                System.Diagnostics.Debug.WriteLine("Ping...");
                debugListener.Resume();
                await Publish(clientName, Topics.PING.Replace("+", client.Options.ClientId));
                await Task.Delay(10000);
            }
        }

        private async Task PublishQueuedLogs()
        {
            foreach (var log in queuedLogs)
                await Publish(Serializer.SerializeJson(log), Topics.Log.Replace("+", ClientId));

            queuedLogs.Clear();
        }

        public async Task Publish(string message, string topic)
        {
            try
            {
                var mqttMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithExactlyOnceQoS()
                    .WithPayload(message)
                    .Build();
                await client.PublishAsync(mqttMessage);
            }
            catch (Exception ex)
            {
                debugListener.Pause();
                System.Diagnostics.Debug.WriteLine(ex);
                debugListener.Resume();
            }
        }

        internal async Task Publish(Stream stream, string topic)
        {
            try
            {
                var mqqtMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithExactlyOnceQoS()
                    .WithPayload(stream)
                    .Build();
                await client.PublishAsync(mqqtMessage);
            }
            catch (Exception ex)
            {
                Logger.Current.WriteError(ex);
            }
        }

        public void Subscribe(params string[] topics)
        {
            topicsToSubcribe.AddRange(topics);

            if (client.IsConnected)
                SubscribePendingTopics();
        }

        public async Task SubscribePendingTopics()
        {
            try
            {
                if (topicsToSubcribe.Count == 0)
                    return;

                foreach(var topic in topicsToSubcribe)
                    if (!subscribedTopics.Contains(topic))
                        subscribedTopics.Add(topic);

                await client.SubscribeAsync(new MqttClientSubscribeOptions()
                {
                    TopicFilters = topicsToSubcribe.Select(x =>
                                        new TopicFilterBuilder()
                                        .WithTopic(x)
                                        .WithExactlyOnceQoS()
                                        .Build()).ToList()
                }, CancellationToken.None);

                Logger.Current.WriteInfo("Subscribed to topics: " + topicsToSubcribe.Aggregate((s1, s2) => $"{s1}, {s2}"));

                topicsToSubcribe.RemoveRange(0, topicsToSubcribe.Count);
            }
            catch (Exception ex)
            {
                Logger.Current.WriteError(ex);
            }
        }

        private async Task OnMessageReceived(MqttApplicationMessageReceivedEventArgs arg)
        {
            debugListener.Pause();
            System.Diagnostics.Debug.WriteLine($"Received mqtt data for Topic: {arg.ApplicationMessage.Topic} and client: {arg.ClientId}");
            debugListener.Resume();
            if(arg.ApplicationMessage.Topic.StartsWith(Topics.PING_RESPONSE.Split('/')[0], StringComparison.Ordinal))
            {
                var clientId = arg.ApplicationMessage.Topic.Split('/')[1];
                if(clientId == ClientId)
                    pingSuccess = true;
            }
            else
                MessageReceived?.Invoke(arg);
        }

        private async Task OnDisconnected(MqttClientDisconnectedEventArgs arg)
        {
            try
            {
                debugListener.Pause();
                System.Diagnostics.Debug.WriteLine("Client Disconnected");
                if (arg.ClientWasConnected)
                {
                    System.Diagnostics.Debug.WriteLine("Trying to reconnect");
                    Subscribe(subscribedTopics.ToArray());
                    await client.ReconnectAsync();
                    System.Diagnostics.Debug.WriteLine("Reconnected");
                    await SubscribePendingTopics();
                    await PublishQueuedLogs();
                }
                debugListener.Resume();
            }
            catch(Exception ex)
            {
                Logger.Current.WriteError(ex);
            }
        }

        async void Log(LogEvent log)
        {
            debugListener.Pause();
            log.PrintLog();
            debugListener.Resume();

            if (IsConnected)
                await Publish(Serializer.SerializeJson(log), Topics.Log.Replace("+", ClientId));
            else
                queuedLogs.Add(log);
        }
    }
}
