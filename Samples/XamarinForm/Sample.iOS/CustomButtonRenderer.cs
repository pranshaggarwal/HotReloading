using System;
using Sample.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(Button), typeof(CustomButtonRenderer))]
namespace Sample.iOS
{
    public class CustomButtonRenderer : ButtonRenderer
    {
    }
}
