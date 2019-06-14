using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Gtk;

namespace VisualStudio.Mac.DataBinding
{
    public class Binding<TBindableWidget, TBindableProperty, TBindingContext, TBindingProperty> where TBindableWidget : INotifyPropertyChanged 
        where TBindingContext : INotifyPropertyChanged
    {
        private readonly TBindableWidget bindable;
        private readonly Expression<Func<TBindableWidget, TBindableProperty>> bindableProperty;
        private readonly TBindingContext bindingContext;
        private readonly Expression<Func<TBindingContext, TBindingProperty>> bindingProperty;

        private PropertyInfo BindablePropertyInfo => GetProperty(bindableProperty);

        private PropertyInfo BindingPropertyInfo => GetProperty(bindingProperty);


        public Binding(TBindableWidget bindable, 
            Expression<Func<TBindableWidget, TBindableProperty>> bindableProperty, 
            TBindingContext bindingContext,
            Expression<Func<TBindingContext, TBindingProperty>> bindingProperty)
        {
            this.bindable = bindable;
            this.bindableProperty = bindableProperty;
            this.bindingContext = bindingContext;
            this.bindingProperty = bindingProperty;

            bindable.PropertyChanged += Bindable_PropertyChanged;
            bindingContext.PropertyChanged += BindingContext_PropertyChanged;

            var targetPropertyInfo = BindablePropertyInfo;
            var sourcePropertyInfo = BindingPropertyInfo;
            targetPropertyInfo.SetValue(bindable, sourcePropertyInfo.GetValue(bindingContext));
        }

        void Bindable_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == BindablePropertyInfo.Name)
            {
                var targetPropertyInfo = BindingPropertyInfo;
                var sourcePropertyInfo = BindablePropertyInfo;
                targetPropertyInfo.SetValue(bindingContext, sourcePropertyInfo.GetValue(bindable));
            }
        }

        void BindingContext_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == BindingPropertyInfo.Name)
            {
                var targetPropertyInfo = BindablePropertyInfo;
                var sourcePropertyInfo = BindingPropertyInfo;
                targetPropertyInfo.SetValue(bindable, sourcePropertyInfo.GetValue(bindingContext));
            }
        }


        private PropertyInfo GetProperty(LambdaExpression expression)
        {
            var memberSelectorExpression = (MemberExpression)expression.Body;
            return (PropertyInfo)memberSelectorExpression.Member;
        }
    }
}
