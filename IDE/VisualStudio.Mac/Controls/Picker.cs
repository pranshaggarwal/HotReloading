using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Gtk;

namespace VisualStudio.Mac.Controls
{
    public class Picker<T> : ComboBox, INotifyPropertyChanged
    {
        private readonly Func<T, string> getDisplayString;
        private IEnumerable<T> items;
        private T selectedItem;

        public IEnumerable<T> Items
        {
            get => items;
            set
            {
                if (items != value)
                {
                    if (items is INotifyCollectionChanged collectionChanged)
                        collectionChanged.CollectionChanged -= OnItemsObservableCollectionChanged;

                    items = value;

                    collectionChanged = items as INotifyCollectionChanged;
                    if (collectionChanged != null)
                        collectionChanged.CollectionChanged += OnItemsObservableCollectionChanged;

                    UpdateItemsSource();
                }
            }
        }

        private bool isInternallyChange;

        public T SelectedItem
        {
            get => selectedItem;
            set
            {
                if (SetProperty(ref selectedItem, value))
                    OnSelectedItemChanged();
            }
        }

        private void OnSelectedItemChanged()
        {
            if (isInternallyChange)
                return;
            var index = Items.IndexOf(SelectedItem);
            Gtk.Application.Invoke(delegate
            {
                Active = index;
            });
        }

        private void OnItemsObservableCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateItemsSource();
            OnSelectedItemChanged();
        }

        public Picker(Func<T, string> getDisplayString = null)
        {
            CellRendererText text = new CellRendererText();
            PackStart(text, true);
            AddAttribute(text, "text", 0);

            if (getDisplayString == null)
                getDisplayString = (t) => t.ToString();
            this.getDisplayString = getDisplayString;

            Changed += Picker_ValueChanged;
        }

        private void UpdateItemsSource()
        {
            Gtk.Application.Invoke(delegate
            {
                ListStore listStore = new ListStore(typeof(string));
                Model = listStore;
                foreach (var item in Items)
                {
                    var text = getDisplayString(item);
                    listStore.AppendValues(text);
                }
            });
        }

        void Picker_ValueChanged(object sender, EventArgs e)
        {
            isInternallyChange = true;
            SelectedItem = Active == -1 ? Items.FirstOrDefault() : Items.ElementAt(Active);
            isInternallyChange = false;
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
