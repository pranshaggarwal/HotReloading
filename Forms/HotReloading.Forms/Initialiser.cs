using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HotReloading.Core;
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

            Device.BeginInvokeOnMainThread(async () => 
            {
                try
                {
                    var currentPage = GetCurrentPage();
                    var mainPage = Application.Current.MainPage;
                    Application.Current.MainPage = new ContentPage();
                    var initMethod = mainPage.GetType().GetMethod("HotReloading_Init");
                    if(initMethod != null)
                        initMethod.Invoke(mainPage, new object[] { });
                    else
                    {
                        if(currentPage != null && currentPage.InstanceMethods.ContainsKey("HotReloading_Init"))
                        {
                            var initDelegate = currentPage.InstanceMethods["HotReloading_Init"];
                            if(initDelegate != null)
                            {
                                initDelegate.DynamicInvoke(mainPage);
                            }
                        }
                    }
                    await Task.Delay(1000);
                    Application.Current.MainPage = mainPage;
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            });
        }

        private static IInstanceClass GetCurrentPage()
        {
            if(Application.Current.MainPage is NavigationPage navigationPage)
            {
                return navigationPage.CurrentPage as IInstanceClass;
            }

            return Application.Current.MainPage as IInstanceClass;
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