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
        }

        protected override void OnAppearing()
        {
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
