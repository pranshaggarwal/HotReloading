namespace HotReloading.Test.TestCodes
{
    public partial class PublicMethodTestClass
    {
        public static void UpdateStaticMethod()
        {
            Tracker.Call("change");
        }

        public static void AddedStaticMethodAndCalledFromSameClass1()
        {
            AddedStaticMethodAndCalledFromSameClass2();
        }

        public static void AddedStaticMethodAndCalledFromSameClass2()
        {
            Tracker.Call("change");
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
