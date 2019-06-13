using System;
using System.Diagnostics;
using System.Reflection;
using HotReloading;
using UIKit;
using Xamarin.Essentials;

namespace Sample.Xamarin.iOS
{
    public class Initialiser
    {
        public static void Init()
        {
            HotReloadingClient.RequestHandled += HotReloadingClient_RequestHandled;

            HotReloadingClient.Run();
        }

        private static async void HotReloadingClient_RequestHandled()
        {
            Debug.WriteLine("New Code change");

            MainThread.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    var currentPage = (UIApplication.SharedApplication.Delegate as UIApplicationDelegate).Window.RootViewController;

                    var type = currentPage.GetType();

                    var ctor = type.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

                    var viewController = ctor[0].Invoke(new object[] { currentPage.Handle });
                    (UIApplication.SharedApplication.Delegate as AppDelegate).Window.RootViewController = viewController as UIViewController;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            });
        }
    }
}
