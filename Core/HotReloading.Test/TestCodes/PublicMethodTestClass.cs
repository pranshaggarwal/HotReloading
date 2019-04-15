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


        public void UpdateInstanceMethod()
        {
            Tracker.Call("change");
        }

        public void AddedInstanceMethodAndCalledFromSameClass()
        {
            AddedInstanceMethodAndCalledFromSameClass1();
        }

        public void AddedInstanceMethodAndCalledFromSameClass1()
        {
            Tracker.Call("change");
        }

        public void AddedStaticMethodAndCalledFromInstanceMethod()
        {
            AddedStaticMethodAndCalledFromInstanceMethod1();
        }

        public static void AddedStaticMethodAndCalledFromInstanceMethod1()
        {
            Tracker.Call("change");
        }

        public void AddedInstanceMethodAndCalledFromAnotherClass()
        {

        }

        public void MethodOverload()
        {

        }

    }
}
