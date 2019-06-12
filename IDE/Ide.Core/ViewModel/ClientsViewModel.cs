using System.Collections.ObjectModel;
using System.Linq;
using Ide.Core.Mqtt;

namespace Ide.Core.ViewModel
{
    public class ClientsViewModel : ViewModelBase
    {
        public ObservableCollection<Client> Clients { get; set; }

        public ClientsViewModel()
        {
            Clients = new ObservableCollection<Client>(Initializer.Clients);

            Initializer.Clients.CollectionChanged += Clients_CollectionChanged;
        }

        void Clients_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (var client in e.NewItems.Cast<Client>())
                        Clients.Add(client);
                    break;
            }
        }
    }
}
