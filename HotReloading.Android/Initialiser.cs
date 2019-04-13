using Android.Content;
using Android.OS;
using Rg.Plugins.Popup;

namespace HotReloading.Android
{
    public static class Initialiser
    {
        public static void Init(Context context, Bundle bundle)
        {
            Popup.Init(context, bundle);
        }
    }
}