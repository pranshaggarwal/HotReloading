using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace HotReloading.Core
{
    public class TcpCommunicatorClient : TcpCommunicator
    {
        private TcpClient client;

        public async Task<bool> Connect(string ip, int port)
        {
            Disconnect();
            client = new TcpClient();
            var tokenSrc = new CancellationTokenSource();
            await client.ConnectAsync(ip, port);
            Receive(client, tokenSrc.Token);
            return true;
        }

        public void Disconnect()
        {
            client?.Close();
            client?.Dispose();
        }
    }
}