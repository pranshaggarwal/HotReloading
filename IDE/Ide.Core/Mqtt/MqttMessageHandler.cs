using System;
using HotReloading.Core;
using Log;
using Mqtt;
using MQTTnet;
using Serializer = HotReloading.Core.Serializer;

namespace Ide.Core.Mqtt
{
    public static class MqttMessageHandler
    {
        public static event Action<Client> ClientConnected;
        public static event Action<string, LogEvent> ReceivedLog;
        public static void HandleMessage(MqttApplicationMessageReceivedEventArgs args)
        {
            if(args.ApplicationMessage.Topic.StartsWith(Topics.PING.Split('/')[0], StringComparison.Ordinal))
            {
                var client = new Client
                {
                    Name = args.ApplicationMessage.ConvertPayloadToString(),
                    Id = args.ApplicationMessage.Topic.Split('/')[1]
                };
                ClientConnected?.Invoke(client);
            }
            else if (args.ApplicationMessage.Topic.StartsWith(Topics.Log.Split('/')[0], StringComparison.Ordinal))
            {
                var json = args.ApplicationMessage.ConvertPayloadToString();
                var logEvent = Serializer.DeserializeJson<LogEvent>(json);
                ReceivedLog?.Invoke(args.ApplicationMessage.Topic.Split('/')[1], logEvent);
            }
        }
    }
}
