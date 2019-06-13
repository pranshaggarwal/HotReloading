using System;
using System.Reflection;
using UIKit;

namespace Sample.Xamarin.iOS
{
    public partial class ViewController : UIViewController
    {
        partial void UIButton197_TouchUpInside(UIButton sender)
        {
            DoSomething();
        }

        public void DoSomething()
        {
            System.Diagnostics.Debug.WriteLine("Button Clicked");
        }

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
