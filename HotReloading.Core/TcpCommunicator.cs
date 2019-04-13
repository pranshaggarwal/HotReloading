using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotReloading.Core
{
    public abstract class TcpCommunicator
    {
        protected ConcurrentDictionary<Guid, Tuple<TcpClient, CancellationTokenSource>> clients =
            new ConcurrentDictionary<Guid, Tuple<TcpClient, CancellationTokenSource>>();

        private string pendingmsg;

        public event EventHandler<string> DataReceived;

        public async Task<bool> Send<T>(T obj)
        {
            var json = Serializer.SerializeJson(obj);
            json += '\0';
            var encoding = new UTF8Encoding(false);
            var bytesToSend = encoding.GetBytes(json);
            foreach (var client in clients)
                if (client.Value.Item1.Connected)
                {
                    //Log.Debug($"Sending to:{client.Key}");
                    await client.Value.Item1.GetStream().WriteAsync(bytesToSend, 0, bytesToSend.Length);
                }
                else
                {
                    //Log.Debug($"Failed to send to:{client.Key}");
                    client.Value.Item1.Close();
                    clients.TryRemove(client.Key, out var removedClient);
                    removedClient?.Item2.Cancel();
                }

            //Improve return if errors
            return true;
        }

        protected void Receive(TcpClient client, CancellationToken cancellationToken)
        {
            var bytes = new byte[1024];
            var bytesRead = 0;

            //Log.Debug("Start receiving updates from ide");
            Task.Run(async () =>
            {
                // Loop to receive all the data sent by the client.
                bytesRead = await client.GetStream().ReadAsync(bytes, 0, bytes.Length, cancellationToken);

                while (bytesRead != 0)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    // Translate data bytes to a UTF8 string.
                    string msg;
                    msg = Encoding.UTF8.GetString(bytes, 0, bytesRead);

                    // Process the data sent by the client.
                    if (pendingmsg != null)
                    {
                        msg = pendingmsg + msg;
                        pendingmsg = null;
                    }

                    var t = msg.LastIndexOf('\0');
                    if (t == -1)
                    {
                        pendingmsg = msg;
                        msg = null;
                    }
                    else if (t != msg.Length - 1)
                    {
                        pendingmsg = msg.Substring(t + 1, msg.Length - t - 1);
                        msg = msg.Substring(0, t);
                    }

                    if (msg != null)
                    {
                        var msgs = msg.Split('\0');
                        foreach (var ms in msgs)
                            if (!string.IsNullOrWhiteSpace(ms))
                                if (DataReceived != null)
                                    DataReceived?.Invoke(this, ms);
                    }

                    //Receive more bytes
                    bytesRead = await client.GetStream().ReadAsync(bytes, 0, bytes.Length, cancellationToken);
                }

                //Log.Debug("Receive stopped, disconnected");
            }, cancellationToken);
        }
    }
}