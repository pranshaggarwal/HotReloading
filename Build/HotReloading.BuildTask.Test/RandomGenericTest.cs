using System;
using NUnit.Framework;

namespace HotReloading.BuildTask.Test
{
    [TestFixture]
    public class RandomGenericTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Reset();
        }

        [Test]
        public void Test_WrapInstanceMethod_WithoutParameter()
        {
            var assemblyToTest = "RandomGenericTest";
            string newAssemblyPath = Helper.GetInjectedAssembly(assemblyToTest);
        }
    }
}
