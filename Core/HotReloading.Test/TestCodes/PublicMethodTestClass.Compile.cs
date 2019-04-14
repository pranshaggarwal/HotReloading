using System;
namespace HotReloading.Test.TestCodes
{
    public partial class PublicMethodTestClass
    {
        public static void UpdateStaticMethod()
        {
            var @delegate = CodeChangeHandler.GetMethodDelegate(typeof(PublicMethodTestClass), nameof(MethodWithNoParameterNoReturnValue));

            if (@delegate != null)
            {
                @delegate.DynamicInvoke();
            }

            Tracker.Call("default");
        }
    }
}
