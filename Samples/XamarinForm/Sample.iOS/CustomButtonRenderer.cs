using System;
using Sample.iOS;
using Xamarin.Forms;

[assembly:ExportRenderer(typeof(Button), typeof(CustomButtonRenderer))]
namespace Sample.iOS
{
    public class CustomButtonRenderer
    {
        public CustomButtonRenderer()
        {
        }
    }
}
