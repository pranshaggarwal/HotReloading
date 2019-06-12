using System;
using Log;
using Mqtt;
using MQTTnet;

namespace HotReloading.Core
{
    //public static class MqttMessageHandler
    //{
    //    private static event Action<LogEvent> LogMessageReceived;

    //    public static void LogMessageHandler(this MqttCommunicatorClient client, Action<LogEvent> handler)
    //    {
    //        LogMessageReceived += handler;
    //    }

    //    public static void HandleMessage(MqttApplicationMessageReceivedEventArgs arg)
    //    {
    //        if (arg.ApplicationMessage.Topic == Topics.Log)
    //        {
    //            var json = arg.ApplicationMessage.ConvertPayloadToString();
    //            LogMessageReceived?.Invoke(Serializer.DeserializeJson<LogEvent>(json));
    //        }
    //    }
    //}
}