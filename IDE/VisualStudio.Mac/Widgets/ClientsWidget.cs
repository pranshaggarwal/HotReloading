using Gtk;
using Ide.Core.Mqtt;
using Ide.Core.ViewModel;
using VisualStudio.Mac.Controls;
using VisualStudio.Mac.DataBinding;
using VisualStudio.Mac.Templates;

namespace VisualStudio.Mac.Widgets
{
    public class ClientsWidget : EventBox
    {
        private readonly ClientsViewModel viewModel;

        public ClientsWidget(ClientsViewModel viewModel)
        {
            this.viewModel = viewModel;
            BuildView();
        }

        private void BuildView()
        {
            var list = new ListView<Client>((t) => new ClientItemTemplate(t));
            list.SetBinding(x => x.Items, viewModel, x => x.Clients);
            Add(list);
        }
    }
}
