using System;
using Sample.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(Button), typeof(CustomButtonRenderer))]
namespace Sample.iOS
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            System.Diagnostics.Debug.WriteLine("OnElementChangedCalled");
            base.OnElementChanged(e);
        }
    }
}
