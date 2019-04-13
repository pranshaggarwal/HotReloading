using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace HotReloading.Core
{
    public class TcpCommunicatorServer : TcpCommunicator
    {
        private TcpListener listener;
        private readonly int serverPort;

        public TcpCommunicatorServer(int serverPort)
        {
            this.serverPort = serverPort;
        }

        public int ClientsCount => clients.Count;

        public event EventHandler ClientConnected;

        public Task<bool> StartListening()
        {
            var taskCompletion = new TaskCompletionSource<bool>();
            Task.Factory.StartNew(() => Run(taskCompletion), TaskCreationOptions.LongRunning);
            return taskCompletion.Task;
        }

        private async Task Run(TaskCompletionSource<bool> tcs)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, serverPort);
                listener.Start();
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }

            //Log.Information($"Tcp server listening at port {serverPort}");
            tcs.SetResult(true);

            // Loop
            for (;;)
            {
                var client = await listener.AcceptTcpClientAsync();
                var token = new CancellationTokenSource();
                Receive(client, token.Token);
                var guid = Guid.NewGuid();
                clients[guid] = new Tuple<TcpClient, CancellationTokenSource>(client, token);
                Debug.WriteLine("New client connection");
                ClientConnected?.Invoke(this, null);
            }
        }

        public void StopListening()
        {
            foreach (var client in clients)
            {
                client.Value.Item1.Close();
                client.Value.Item2.Cancel();
            }

            clients.Clear();
            listener.Stop();
        }
    }
}