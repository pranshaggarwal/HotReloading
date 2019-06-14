using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using HotReloading;
using UIKit;
using Xamarin.Essentials;

namespace Reloading.iOS
{
    public class Initialiser
    {
        public static void Init()
        {
            HotReloadingClient.RequestHandled += HotReloadingClient_RequestHandled;

            HotReloadingClient.Run();
        }

        private static void HotReloadingClient_RequestHandled()
        {
            Debug.WriteLine("New Code change");

            MainThread.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    var currentPage = (UIApplication.SharedApplication.Delegate as UIApplicationDelegate).Window.RootViewController;

                    var type = currentPage.GetType();

                    var parametersField = type.GetField("hotReloading_Ctor_Parameters", BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance);
                    var parameters = parametersField.GetValue(currentPage) as ArrayList;

                    var ctor = GetSuitableCtor(type, parameters);

                    var viewController = ctor.Invoke(parameters.ToArray());
                    (UIApplication.SharedApplication.Delegate as UIApplicationDelegate).Window.RootViewController = viewController as UIViewController;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            });
        }

        private static ConstructorInfo GetSuitableCtor(Type type, ArrayList parameters)
        {
            var ctors = type.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

            ctors = ctors.Where(x => x.GetParameters().Length == parameters.Count).ToArray();

            foreach(var ctor in ctors)
            {
                var ctorParameters = ctor.GetParameters();
                var bestMatch = true;

                for(int index = 0; index < ctorParameters.Length; index++)
                {
                    if(!ctorParameters[index].ParameterType.IsAssignableFrom(parameters[index].GetType()))
                    {
                        bestMatch = false;
                        break;
                    }
                }
                if (bestMatch)
                    return ctor;
            }

            throw new Exception("No Suitable constructor found");
        }
    }
}
