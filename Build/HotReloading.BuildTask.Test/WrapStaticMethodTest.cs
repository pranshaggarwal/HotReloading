using System;
using System.IO;
using System.Linq;
using System.Reflection;
using BaseAssembly;
using FluentAssertions;
using NUnit.Framework;

namespace HotReloading.BuildTask.Test
{
    [TestFixture]
    public class WrapStaticMethodTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Reset();
        }

        [Test]
        public void Test_WrapStaticMethod_WithoutParameter()
        {
            var assemblyToTest = "WrapStaticMethodWithoutParameter";
            string newAssemblyPath = Helper.GetInjectedAssembly(assemblyToTest);

            var assembly = Assembly.LoadFrom(newAssemblyPath);

            var type = assembly.GetType(Helper.GetFullClassname(assemblyToTest));

            var testMethod = type.GetMethod("TestMethod");

            testMethod.Invoke(null, new object[] { });

            Tracker.LastValue.Should().Be("default");

            File.Delete(newAssemblyPath);
        }
    }
}
