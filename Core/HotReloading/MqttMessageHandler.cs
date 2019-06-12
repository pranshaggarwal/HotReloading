using System;
using HotReloading.Core;
using Mqtt;
using MQTTnet;
using Serializer = HotReloading.Core.Serializer;

namespace HotReloading
{
    internal static class MqttMessageHandler
    {
        public static event Action<CodeChangeMessage> NewCodeChangeFound;
        public static void HandleMessage(MqttApplicationMessageReceivedEventArgs args)
        {
            if (args.ApplicationMessage.Topic == Topics.CODE_CHANGE)
            {
                var json = args.ApplicationMessage.ConvertPayloadToString();
                var codeChangeMessage = Serializer.DeserializeJson<CodeChangeMessage>(json);
                NewCodeChangeFound?.Invoke(codeChangeMessage);
            }
        }
    }
}