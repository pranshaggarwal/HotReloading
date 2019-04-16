using System;
using BaseAssembly;

namespace WrapStaticMethodWithoutParameter
{
    public class TestClass
    {
        public static void TestMethod()
        {
            Tracker.Call("default");
        }
    }
}
