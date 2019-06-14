using System;
using Gtk;
using Ide.Core.Mqtt;
using VisualStudio.Mac.Controls;
using VisualStudio.Mac.Extensions;

namespace VisualStudio.Mac.Templates
{
    public class ClientItemTemplate : HBox
    {
        private readonly Client client;

        public ClientItemTemplate(Client client)
        {
            this.client = client;
            BuildView();
        }

        private void BuildView()
        {
            var vBox = new VBox();
            vBox.PackStart(new CustomLabel(client.Name) { FontSize = 14, FontAttributes = CoreUI.FontAttributes.Bold }, false, false, 2);
            vBox.PackStart(new CustomLabel(client.Id) { TextColor = System.Drawing.Color.DarkGray}, false, false, 2);
            PackStart(vBox, false, false, 2);
        }
    }
}
