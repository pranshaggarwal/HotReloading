using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace VisualStudio.Mac.DataBinding
{
    public static class BindingExtension
    {
        private static List<object> bindings = new List<object>();
        public static void SetBinding<TBindableWidget, TBindableProperty, TBindingContext, TBindingProperty>(this TBindableWidget bindable,
            Expression<Func<TBindableWidget, TBindableProperty>> bindableProperty,
            TBindingContext bindingContext,
            Expression<Func<TBindingContext, TBindingProperty>> bindingProperty)
            where TBindableWidget : INotifyPropertyChanged
            where TBindingContext : INotifyPropertyChanged
        {
            var binding = new Binding<TBindableWidget, TBindableProperty, TBindingContext, TBindingProperty>(bindable, bindableProperty, bindingContext, bindingProperty);
            bindings.Add(binding);
        }
    }
}
