using System;
namespace HotReloading.Test.TestCodes
{
    public partial class FieldTestClass
    {
        public static void TestField1()
        {
            var methodKey = Core.Helper.GetMethodKey(nameof(TestField1));
            var @delegate = Runtime.GetMethodDelegate(typeof(FieldTestClass), methodKey);

            if (@delegate != null)
            {
                @delegate.DynamicInvoke();
                return;
            }
        }
    }
}
