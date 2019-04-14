using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HotReloading.Core;

namespace HotReloading
{
    public class HotReloadingClient
    {
        private readonly TcpCommunicatorClient client;

        private bool isRunning;

        public HotReloadingClient()
        {
            client = new TcpCommunicatorClient();
            client.DataReceived += HandleDataReceived;
        }


        private static HotReloadingClient Instance { get; set; }

        public static event Action<string> ParsingError;
        public static event Action<string> CompileError;
        public static event Action RequestHandled;

        private void HandleDataReceived(object sender, string request)
        {
            try
            {
                var methodRequest = Serializer.DeserializeJson<CodeChangeRequest>(request);

                if (methodRequest.ParsingError != null)
                {
                    ParsingError?.Invoke(methodRequest.ParsingError);
                }
                else if (methodRequest.CompileError != null)
                {
                    CompileError?.Invoke(methodRequest.CompileError);
                }
                else
                {
                    CodeChangeHandler.HandleRequest(methodRequest);
                    RequestHandled?.Invoke();
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
            Instance = new HotReloadingClient();
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
                await client.Connect(ideIP, idePort);
                Debug.WriteLine("Connected to: " + ideIP);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}