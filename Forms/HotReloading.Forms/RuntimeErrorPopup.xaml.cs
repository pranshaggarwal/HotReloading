using Rg.Plugins.Popup.Pages;

namespace HotReloading.Forms
{
    public partial class RuntimeErrorPopup : PopupPage
    {
        public RuntimeErrorPopup(string stackTrace)
        {
            InitializeComponent();

            StackTraceLabel.Text = stackTrace;
        }
    }
}