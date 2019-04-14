using System;
namespace HotReloading.Test.TestCodes
{
    public partial class PublicMethodTestClass
    {
        public static void UpdateStaticMethod()
        {
            var @delegate = CodeChangeHandler.GetMethodDelegate(typeof(PublicMethodTestClass), nameof(UpdateStaticMethod));

            if (@delegate != null)
            {
                @delegate.DynamicInvoke();
                return;
            }

            Tracker.Call("default");
        }
    }
}
