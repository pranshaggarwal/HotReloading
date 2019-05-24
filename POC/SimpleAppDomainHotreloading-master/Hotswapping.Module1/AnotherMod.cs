using System;
using Hotswapping.ModApi;

namespace Hotswapping.Module1
{
    public class AnotherMod : IMod
    {
        public void Init()
        {
            var test = new TestClass();
            test.Start();
        }
    }
}