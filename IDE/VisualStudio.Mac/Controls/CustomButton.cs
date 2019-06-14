using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Gtk;
using System.Windows.Input;

namespace VisualStudio.Mac.Controls
{
    public class CustomButton : Button, INotifyPropertyChanged
    {
        private ICommand command;
        private object commandParameter;

        public ICommand Command
        {
            get => command;
            set => SetProperty(ref command, value);
        }

        public object CommandParameter
        {
            get => commandParameter;
            set => SetProperty(ref commandParameter, value);
        }

        public CustomButton(string label) : base(label)
        {

        }

        protected override void OnClicked()
        {
            base.OnClicked();

            if (Command != null && Command.CanExecute(CommandParameter))
                Command.Execute(CommandParameter);
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetProperty<TProperty>(ref TProperty storage, TProperty value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<TProperty>.Default.Equals(storage, value)) return false;

            storage = value;
            RaisePropertyChanged(propertyName);

            return true;
        }

        protected void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
        #endregion
    }
}
