using System;
namespace HotReloading.Test.TestCodes
{
    public partial class PublicMethodTestClass1
    {
        public static void AddedStaticMethodAndCalledFromAnotherClass()
        {
            var @delegate = CodeChangeHandler.GetMethodDelegate(typeof(PublicMethodTestClass1), nameof(AddedStaticMethodAndCalledFromAnotherClass));

            if (@delegate != null)
            {
                @delegate.DynamicInvoke();
                return;
            }
        }
    }
}
