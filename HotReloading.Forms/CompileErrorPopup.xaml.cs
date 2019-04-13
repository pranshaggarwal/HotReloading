using Rg.Plugins.Popup.Pages;

namespace HotReloading.Forms
{
    public partial class CompileErrorPopup : PopupPage
    {
        public CompileErrorPopup(string errorMessage)
        {
            InitializeComponent();

            ErrorLabel.Text = errorMessage;
        }
    }
}