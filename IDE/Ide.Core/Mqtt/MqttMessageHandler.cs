using System;
using HotReloading.Core;
using Log;
using MQTTnet;
using MqttCore = Mqtt;

namespace Ide.Core.Mqtt
{
    public static class MqttMessageHandler
    {
        public static event Action<Client> ClientConnected;
        public static event Action<string, LogEvent> ReceivedLog;
        public static void HandleMessage(MqttApplicationMessageReceivedEventArgs args)
        {
            if(args.ApplicationMessage.Topic == MqttCore.Topics.PING)
            {
                var client = new Client
                {
                    Name = args.ApplicationMessage.ConvertPayloadToString(),
                    Id = args.ClientId
                };
                ClientConnected?.Invoke(client);
            }
            else if (args.ApplicationMessage.Topic == Topics.Log)
            {
                var json = args.ApplicationMessage.ConvertPayloadToString();
                var logEvent = Serializer.DeserializeJson<LogEvent>(json);
                ReceivedLog?.Invoke(args.ClientId, logEvent);
            }
        }
    }
}
