using MonoDevelop.Components;
using MonoDevelop.Ide.Gui;
using VisualStudio.Mac.Controls;
using Gtk;
using VisualStudio.Mac.Widgets;
using Ide.Core.ViewModel;

namespace VisualStudio.Mac
{
    public class HotReloadingPad : PadContent
    {
        private Notebook control;
        private LogsViewModel logsViewModel;
        //private ClientsViewModel clientsViewModel;

        public HotReloadingPad()
        {
            logsViewModel = new LogsViewModel();
            //clientsViewModel = new ClientsViewModel();
        }

        public override Control Control => control;

        protected override void Initialize(IPadWindow window)
        {
            base.Initialize(window);

            var tabControl = new TabbedControl();
            tabControl.AddPage(new LogsWidget(logsViewModel), "Logs");
            //tabControl.AddPage(new ClientsWidget(clientsViewModel), "Clients");
            control = tabControl;
            control.ShowAll();
        }
    }
}