using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Ide.Core.Mqtt;
using Log;

namespace Ide.Core.ViewModel
{
    public class LogsViewModel : ViewModelBase
    {
        private Client selectedClient;
        public ObservableCollection<Client> Clients { get; set; }
        public static ObservableCollection<KeyValuePair<string, LogEvent>> Logs { get; set; } = new ObservableCollection<KeyValuePair<string, LogEvent>>();

        public Client SelectedClient
        {
            get => selectedClient;
            set => SetProperty(ref selectedClient, value);
        }

        public LogsViewModel()
        {
            Clients = new ObservableCollection<Client>(Initializer.Clients);

            SelectedClient = Clients.FirstOrDefault();

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
