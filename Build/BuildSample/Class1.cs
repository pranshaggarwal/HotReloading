using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using AnotherTestProject;
using Xamarin.Forms;

namespace BuildSample
{
    public class Class1 : BaseClass, INotifyPropertyChanged
    {
        public Xamarin.Forms.Color TestColor { get; set; }

        public static BindableProperty HasBadgeProperty = BindableProperty
                .CreateAttached("BadgeText",
                                typeof(bool),
                                typeof(Class1), false);

        public static readonly BindableProperty PopupViewTemplateProperty = BindableProperty.Create("PopupViewTemplate", typeof(DataTemplate), typeof(Class1));


        public DataTemplate PopupViewTemplate
        {
            get
            {
                return (DataTemplate)GetValue(PopupViewTemplateProperty);
            }
            set
            {
                SetValue(PopupViewTemplateProperty, value);
            }
        }

        private void SetValue(BindableProperty popupViewTemplateProperty, object value)
        {
        }

        private object GetValue(BindableProperty popupViewTemplateProperty)
        {
            return null;
        }

        public Class1()
        {
            InstanceMethod();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void InstanceMethod()
        {
            Debug.WriteLine("Hello");
        }

        private string InstanceMethod2(string test)
        {
            return test;
        }

        public static void StaticMethod()
        {
            var test = new TestClass();
        }

        public static string StaticMethod1(string test)
        {
            return test;
        }

        [GeneratedCode("Test", "0.0.0.0")]
        public void GeneratedCode()
        {
        }
    }

    public enum TestEnum
    {
        Default
    }
}