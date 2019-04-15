using System;
namespace HotReloading.Test.TestCodes
{
    public partial class PublicMethodTestClass1
    {
        public static void AddedStaticMethodAndCalledFromAnotherClass()
        {
            PublicMethodTestClass.UpdateStaticMethod();
        }
    }
}
