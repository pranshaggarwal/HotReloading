using System;
using System.Collections;
using System.Reflection;
using HotReloading.Core;
using UIKit;

namespace Sample.Xamarin.iOS
{
    public partial class ViewController : UIViewController
    {
        private int count = 0;
        partial void UIButton197_TouchUpInside(UIButton sender)
        {
            DoSomething();
        }

        public void DoSomething()
        {
            count++;
            System.Diagnostics.Debug.WriteLine("Count2: " + count);
            MyButton.SetTitle("Button" + count, UIControlState.Normal);
        }

        partial void UIButton594_TouchUpInside(UIButton sender)
        {
            DoSomething1();
        }

        public void DoSomething1()
        {
            System.Diagnostics.Debug.WriteLine("Count3:" + count);
        }

        protected ViewController(IntPtr handle)
            : base(handle)
        {
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
