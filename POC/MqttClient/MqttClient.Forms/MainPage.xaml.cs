using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using Xamarin.Forms;

namespace MqttClient.Forms
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            Debug.WriteLine("Client");
            var factory = new MqttFactory();
            var client = factory.CreateMqttClient();
            client.UseConnectedHandler(async (arg) =>
            {
                Debug.WriteLine("Connected with server");
                //await client.SubscribeAsync(new MqttClientSubscribeOptions()
                //{
                //    TopicFilters = new System.Collections.Generic.List<MQTTnet.TopicFilter>
                //    {
                //        new TopicFilterBuilder().WithTopic("HotReloading").WithExactlyOnceQoS().Build()
                //    }
                //}, CancellationToken.None);
            });
            client.UseApplicationMessageReceivedHandler((arg) =>
            {
                Debug.WriteLine("Data Received: " + arg.ApplicationMessage.ConvertPayloadToString());
            });

            var clientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer("192.168.1.155", 8076)
                .Build();

            await client.ConnectAsync(clientOptions);

            var message = new MqttApplicationMessageBuilder()
                .WithTopic("HotReloading")
                .WithExactlyOnceQoS()
                .WithPayload("Test data")
                .Build();
            await client.PublishAsync(message);

            Debug.WriteLine("Press any key to close app");

            await client.DisconnectAsync();
        }
    }
}
