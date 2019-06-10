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
            Add(picker);

            foreach (var client in viewModel.Clients)
            {
                CreateLogView(client);
            }

            UpdateSelectedItem();

            //var log = new LogView();
            //Add(log);
            //var monitor = log.GetProgressMonitor();
            //Monitor = monitor;
            //monitor.Log.WriteLine("log");
            //monitor.ErrorLog.WriteLine("error");
        }
        private bool isCreatingLogView;
        private object lockObject = new object();

        private void CreateLogView(Client client)
        {
            if (logViews.ContainsKey(client.Id))
                return;
            System.Diagnostics.Debug.WriteLine("Creating View started");
            //isCreatingLogView = true;
            System.Diagnostics.Debug.WriteLine("IsBackground Thread " + System.Threading.Thread.CurrentThread.IsBackground);

            if(System.Threading.Thread.CurrentThread.IsBackground)
            {
                Gtk.Application.Invoke(delegate
                {
                    //lock (lockObject)
                    {
                        System.Diagnostics.Debug.WriteLine("Creating logview");
                        var logView = new LogView();
                        logViews.Add(client.Id, logView);
                        foreach (var logEvent in LogsViewModel.Logs.Where(x => x.Key == client.Id))
                        {
                            WriteLog(logView, logEvent.Value);
                        }
                        isCreatingLogView = false;
                        System.Diagnostics.Debug.WriteLine("logview created");
                    }
                }
                );
            }
            else
            //Gtk.Application.Invoke(delegate
            {
                //lock (lockObject)
                {
                    System.Diagnostics.Debug.WriteLine("Creating logview");
                    var logView = new LogView();
                    logViews.Add(client.Id, logView);
                    foreach (var logEvent in LogsViewModel.Logs.Where(x => x.Key == client.Id))
                    {
                        WriteLog(logView, logEvent.Value);
                    }
                    isCreatingLogView = false;
                    System.Diagnostics.Debug.WriteLine("logview created");
                }
            }
            //);
        }

        private void WriteLog(LogView logView, LogEvent logEvent)
        {
            var monitor = logView.GetProgressMonitor(false);
            if (logEvent.LogLevel == LogLevel.Error)
            {
                monitor.ErrorLog.WriteLine(logEvent.Message);
            }
            else if (logEvent.LogLevel == LogLevel.Info)
            {
                monitor.Log.WriteLine(logEvent.Message);
                monitor.WarningLog.WriteLine("Warning");
                monitor.DebugLog.WriteLine("Debug Log");
            }
            else if (logEvent.LogLevel == LogLevel.Warning)
                monitor.Log.WriteLine(logEvent.Message);
        }

        private async void UpdateSelectedItem()
        {
            if(viewModel.SelectedClient != null)
            {
                while(isCreatingLogView)
                {
                    System.Diagnostics.Debug.WriteLine("Is Creating LogView true");
                    await Task.Delay(500);
                }
                //Gtk.Application.Invoke(delegate
                {
                    System.Diagnostics.Debug.WriteLine("No view is creating");
                    if (currentLogView != null)
                    {
                        Remove(currentLogView);
                        currentLogView.HideAll();
                        System.Diagnostics.Debug.WriteLine("View hidden");
                    }
                    if (logViews.Count <= 0)
                    {

                    }
                    System.Diagnostics.Debug.WriteLine("Adding LogView: " + viewModel.SelectedClient.Id);
                    var logView = logViews.FirstOrDefault(x => x.Key == viewModel.SelectedClient.Id).Value;
                    //var monitor = logView.GetProgressMonitor();
                    //monitor.Log.WriteLine("test1");
                    //monitor.Log.WriteLine("test2");
                    Add(logView);
                    currentLogView = logView;
                    logView.ShowAll();
                }
                //);
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
