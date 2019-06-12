using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using HotReloading.Core;
using Log;
using Mqtt;
using Serializer = HotReloading.Core.Serializer;

namespace HotReloading
{
    public class HotReloadingClient
    {
        private MqttCommunicatorClient mqttClient;

        private bool isRunning;

        public HotReloadingClient(string address, int port)
        {
            mqttClient = new MqttCommunicatorClient(address, port);
        }


        private static HotReloadingClient Instance { get; set; }

        public static event Action<string> ParsingError;
        public static event Action<string> CompileError;
        public static event Action RequestHandled;

        private void HandleDataReceived(object sender, string messageJson)
        {
            try
            {
                var message = Serializer.DeserializeJson<CodeChangeMessage>(messageJson);

                if(message.Error == null)
                {
                    Runtime.HandleCodeChange(message.CodeChange);
                    RequestHandled?.Invoke();
                }
                else if (message.Error.ParsingError != null)
                {
                    ParsingError?.Invoke(message.Error.ParsingError);
                }
                else if (message.Error.CompileError != null)
                {
                    CompileError?.Invoke(message.Error.CompileError);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                ParsingError?.Invoke(ex.Message);
            }
        }

        public static async Task<bool> Run(string ideIP = "127.0.0.1", int idePort = Constants.DEFAULT_PORT)
        {
            Instance = new HotReloadingClient(ideIP, idePort);
            return await Instance.RunInternal(ideIP, idePort);
        }

        internal async Task<bool> RunInternal(string ideIP, int idePort)
        {
            if (isRunning) return true;

            await RegisterDevice(ideIP, idePort);
            isRunning = true;
            return true;
        }

        private async Task RegisterDevice(string ideIP, int idePort)
        {
            try
            {
                await mqttClient.ConnectAsync("Phone Client");
                Debug.WriteLine("Connected to: " + ideIP);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}