using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sample
{
    public partial class FirstPage : ContentPage
    {
        private Button button;
        public FirstPage()
        {
            SetupView();
        }

        public void HotReloading_Init()
        {
            SetupView();
        }

        public void SetupView()
        {
            button = new Button()
            {
                Text = "Click Me1",
                TextColor = Color.Red,
                FontAttributes = FontAttributes.Italic,
                FontSize = 24
            };

            Content = button;

            var type = Type.GetType("Acr.UserDialogs.IUserDialogs, Acr.UserDialogs");
        }

        protected override void OnAppearing()
        {
            //Acr.UserDialogs.UserDialogs.Instance.ShowLoading();

            var person = new Sample.Core.Person();
            TestAsync();
        }

        public static async void TestAsync()
        {
            await Task.Delay(1000);
            System.Diagnostics.Debug.WriteLine("test async2");
        }
    }
}
