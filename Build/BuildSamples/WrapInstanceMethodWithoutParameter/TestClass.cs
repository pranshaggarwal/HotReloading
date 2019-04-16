using System;
using BaseAssembly;

namespace WrapInstanceMethodWithoutParameter
{
    public class TestClass
    {
        public void TestMethod()
        {
            Tracker.Call("default");
        }
    }
}
