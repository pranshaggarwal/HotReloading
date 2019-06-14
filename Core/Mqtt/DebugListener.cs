using System;
using System.Diagnostics;
using Log;

namespace Mqtt
{
    public class DebugListener : TraceListener
    {
        private MqttCommunicatorClient mqttClient;
        private bool pause;

        public DebugListener(MqttCommunicatorClient mqttClient)
        {
            this.mqttClient = mqttClient;
        }

        public override async void Write(string message)
        {
            if (pause)
                return;

            var log = new LogEvent(LogLevel.Debug, message);
            if (mqttClient.IsConnected)
                await mqttClient.Publish(Serializer.SerializeJson(log), Topics.Log.Replace("+", mqttClient.ClientId));
        }

        public override async void WriteLine(string message)
        {
            if (pause)
                return;

            var log = new LogEvent(LogLevel.Debug, message);
            if(mqttClient.IsConnected)
                await mqttClient.Publish(Serializer.SerializeJson(log), Topics.Log.Replace("+", mqttClient.ClientId));
        }

        public void Pause()
        {
            pause = true;
        }

        public void Resume()
        {
            pause = false;
        }
    }
}
