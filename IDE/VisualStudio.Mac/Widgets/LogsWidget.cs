using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gtk;
using Ide.Core.Mqtt;
using Ide.Core.ViewModel;
using Log;
using MonoDevelop.Core.Execution;
using VisualStudio.Mac.Controls;
using VisualStudio.Mac.DataBinding;

namespace VisualStudio.Mac.Widgets
{
    public class LogsWidget : VBox
    {
        private readonly LogsViewModel viewModel;

        private Dictionary<string, LogView> logViews = new Dictionary<string, LogView>();

        private LogView currentLogView;

        public LogsWidget(LogsViewModel viewModel)
        {
            this.viewModel = viewModel;
            BuildView();
            viewModel.Clients.CollectionChanged += Clients_CollectionChanged;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            LogsViewModel.Logs.CollectionChanged += Logs_CollectionChanged;
        }

        private void BuildView()
        {
            var picker = new Picker<Client>((t) => t.Name);
            picker.SetBinding((x) => x.Items, viewModel, (x) => x.Clients);
            picker.SetBinding(x => x.SelectedItem, viewModel, x => x.SelectedClient);
            PackStart(picker, false, false, 0);

            foreach (var client in viewModel.Clients)
            {
                CreateLogView(client);
            }

            UpdateSelectedItem();
        }

        private void CreateLogView(Client client)
        {
            if (logViews.ContainsKey(client.Id))
                return;

            if(System.Threading.Thread.CurrentThread.IsBackground)
            {
                Gtk.Application.Invoke(delegate
                {
                    var logView = new LogView();
                    logViews.Add(client.Id, logView);
                    foreach (var logEvent in LogsViewModel.Logs.Where(x => x.Key == client.Id))
                    {
                        WriteLog(logView, logEvent.Value);
                    }
                }
                );
            }
            else
            {
                var logView = new LogView();
                logViews.Add(client.Id, logView);
                foreach (var logEvent in LogsViewModel.Logs.Where(x => x.Key == client.Id))
                {
                    WriteLog(logView, logEvent.Value);
                }
            }
        }

        private void WriteLog(LogView logView, LogEvent logEvent)
        {
            var monitor = logView.GetProgressMonitor(false);
            if (logEvent.LogLevel == LogLevel.Error)
                monitor.ErrorLog.WriteLine(logEvent.Message);
            else if (logEvent.LogLevel == LogLevel.Debug)
                monitor.DebugLog.WriteLine(logEvent.Message);
            else if (logEvent.LogLevel == LogLevel.Warning)
                monitor.WarningLog.WriteLine(logEvent.Message);
            else
                monitor.Log.WriteLine(logEvent.Message);

        }

        private async void UpdateSelectedItem()
        {
            if(viewModel.SelectedClient != null)
            {
                    if (currentLogView != null)
                    {
                        Remove(currentLogView);
                        currentLogView.HideAll();
                    }
                    var logView = logViews.FirstOrDefault(x => x.Key == viewModel.SelectedClient.Id).Value;
                    Add(logView);
                    currentLogView = logView;
                    logView.ShowAll();
            }
        }

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(viewModel.SelectedClient))
            {
                UpdateSelectedItem();
            }
        }

        void Logs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach(var keyValuePair in e.NewItems.Cast<KeyValuePair<string, LogEvent>>())
                {
                    var logView = logViews.FirstOrDefault(x => x.Key == keyValuePair.Key).Value;
                    WriteLog(logView, keyValuePair.Value);
                }
            }
        }

        void Clients_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach(var client in e.NewItems.Cast<Client>())
                {
                    CreateLogView(client);
                }
            }
        }
    }
}
