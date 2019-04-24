using System;
namespace HotReloading.Test.TestCodes
{
    public partial class PublicMethodTestClass1
    {
        public static void AddedStaticMethodAndCalledFromAnotherClass()
        {
            PublicMethodTestClass.UpdateStaticMethod();
        }

        public void AddedInstanceMethodAndCalledFromAnotherClass()
        {
            var instance = new PublicMethodTestClass();
            instance.UpdateInstanceMethod();
        }
    }
}
