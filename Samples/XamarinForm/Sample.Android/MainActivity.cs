using Android.App;
using Android.Content.PM;
using Android.OS;
using HotReloading.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Sample.Droid
{
    [Activity(Label = "Sample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Initialiser.Init(this, savedInstanceState);

            Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}