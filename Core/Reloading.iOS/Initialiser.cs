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
                    var currentViewController = (UIApplication.SharedApplication.Delegate as UIApplicationDelegate).Window.RootViewController;

                    var type = currentViewController.GetType();

                    var parametersField = type.GetField("hotReloading_Ctor_Parameters", BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance);
                    var parameters = parametersField.GetValue(currentViewController) as ArrayList;

                    var ctor = GetSuitableCtor(type, parameters);

                    var newViewController = ctor.Invoke(parameters.ToArray()) as UIViewController;

                    CopyData(currentViewController, newViewController);

                    (UIApplication.SharedApplication.Delegate as UIApplicationDelegate).Window.RootViewController = newViewController;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            });
        }

        private static void CopyData(object oldObj, object newObj)
        {
            var oldType = oldObj.GetType();
            var newType = newObj.GetType();

            if (oldType != newType)
                throw new Exception("Cannot copy data between to different type");

            var fields = oldType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            foreach(var field in fields)
            {
                var oldValue = field.GetValue(oldObj);
                field.SetValue(newObj, oldValue);
            }
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
