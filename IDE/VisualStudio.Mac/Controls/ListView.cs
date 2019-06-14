using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Gtk;
using System.Linq;
using VisualStudio.Mac.Extensions;

namespace VisualStudio.Mac.Controls
{
    public class ListView<T> : ScrolledWindow, INotifyPropertyChanged
    {
        private ArrayList internalItems;
        private List<ToggleButton> selectedButtons;
        private IEnumerable<T> items;
        private VBox vBox;
        private readonly Func<T, Widget> getItemTemplate;

        public IEnumerable<T> Items
        {
            get => items;
            set
            {
                if (items != value)
                {
                    var collectionChanged = items as INotifyCollectionChanged;
                    if (collectionChanged != null)
                        collectionChanged.CollectionChanged -= OnItemsObservableCollectionChanged;

                    items = value;

                    collectionChanged = items as INotifyCollectionChanged;
                    if(collectionChanged != null)
                        collectionChanged.CollectionChanged += OnItemsObservableCollectionChanged;

                    AddItems();
                }
            }
        }

        public ListView(Func<T, Widget> getItemTemplate)
        {
            internalItems = new ArrayList();
            selectedButtons = new List<ToggleButton>();
            vBox = new VBox();
            AddWithViewport(vBox);
            this.getItemTemplate = getItemTemplate;
        }

        private void OnItemsObservableCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateItems();
        }

        private void UpdateItems()
        {
            Clear();
            AddItems();
        }

        private void AddItems()
        {
            Gtk.Application.Invoke(delegate
            {
                foreach (var item in Items)
                {
                    AddItemToView(item);
                }

            });
        }


        private void Clear()
        {
            Gtk.Application.Invoke(delegate
            {
                foreach (var child in vBox.Children)
                    vBox.RemoveFromContainer(child);
            });
        }

        private void AddItemToView(T item)
        {
            if (vBox.Children.Count() >= 1)
            {
                var boxView = new BoxView();
                boxView.SetBackgroundColor(System.Drawing.Color.Gray);
                boxView.HeightRequest = 1;
                vBox.PackStart(boxView, false, false, 2);
            }

            var itemTemplate = getItemTemplate(item);

            vBox.PackStart(itemTemplate, false, false, 2);
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
