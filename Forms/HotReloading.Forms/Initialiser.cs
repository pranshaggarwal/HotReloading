using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace HotReloading.Forms
{
    public static class Initialiser
    {
        public static void Init()
        {
            HotReloadingClient.CompileError += HotReloadingClient_CompileError;
            HotReloadingClient.ParsingError += HotReloadingClient_ParsingError;
            HotReloadingClient.RequestHandled += HotReloadingClient_RequestHandled;

            HotReloadingClient.Run();
        }

        private static async void HotReloadingClient_CompileError(string errorMessage)
        {
            await OpenPopup(new CompileErrorPopup(errorMessage));
        }

        private static async void HotReloadingClient_ParsingError(string errorMessage)
        {
            Debug.WriteLine(errorMessage);
            await OpenPopup(new ParsingErrorPopup());
        }

        private static async void HotReloadingClient_RequestHandled()
        {
            await ClosePopup();
            Debug.WriteLine("New Code change");

            Device.BeginInvokeOnMainThread(() => 
            {
                try
                {
                    var mainPage = Application.Current.MainPage;
                    Application.Current.MainPage = new ContentPage();
                    var setupView = mainPage.GetType().GetMethod("SetupView");
                    if(setupView != null)
                        setupView.Invoke(mainPage, new object[] { });
                    Application.Current.MainPage = mainPage;
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            });
        }

        private static async Task OpenPopup(PopupPage popupPage)
        {
            await ClosePopup();
            await PopupNavigation.Instance.PushAsync(popupPage);
        }

        private static async Task ClosePopup()
        {
            var popupPage = PopupNavigation.Instance.PopupStack.LastOrDefault();

            if (popupPage is ParsingErrorPopup || popupPage is CompileErrorPopup || popupPage is RuntimeErrorPopup)
                await PopupNavigation.Instance.PopAsync();
        }
    }
}