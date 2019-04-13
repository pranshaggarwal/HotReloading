using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PrismSample.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string text;

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public ICommand ClickCommand => new Command(Click);

        private void Click(object obj)
        {
            Text = "test1";
        }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            System.Diagnostics.Debug.WriteLine("viewmodel");
        }
    }
}
