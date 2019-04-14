namespace HotReloading.Test.TestCodes
{
    public partial class PublicMethodTestClass
    {
        public static void UpdateStaticMethod()
        {
            Tracker.Call("change");
        }

        public void AddedStaticMethodAndCalledFromSameClass()
        {

        }

        public void AddedStaticMethodAndCalledFromAnotherClass()
        {

        }

        public void UpdateInstanceMethod()
        {

        }

        public void AddedInstanceMethodAndCalledFromSameClass()
        {

        }

        public void AddedInstanceMethodAndCalledFromAnotherClass()
        {

        }

        public void MethodOverload()
        {

        }

    }
}
