using System;
using System.Reflection;
using BaseAssembly;
using FluentAssertions;
using NUnit.Framework;

namespace HotReloading.BuildTask.Test
{
    [TestFixture]
    public class BaseMethodCallTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Reset();
        }

        [Test]
        public void Test_BaseMethodCallTest1()
        {
            var assemblyToTest = "BaseMethodCallTest1";
            string newAssemblyPath = Helper.GetInjectedAssembly(assemblyToTest);

            var assembly = Assembly.LoadFrom(newAssemblyPath);

            var type = assembly.GetType(Helper.GetFullClassname(assemblyToTest));

            var methodName = "HotReloadingBase_BaseMethod";

            var baseMethod = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

            var instance = Activator.CreateInstance(type);

            baseMethod.Invoke(instance, new object[] { });

            Tracker.LastValue.Should().Be("default");
        }

        [Test]
        public void Test_BaseMethodCallTest2()
        {
            var assemblyToTest = "BaseMethodCallTest2";
            string newAssemblyPath = Helper.GetInjectedAssembly(assemblyToTest);

            var assembly = Assembly.LoadFrom(newAssemblyPath);

            var type = assembly.GetType(Helper.GetFullClassname(assemblyToTest));

            var methodName = "HotReloadingBase_BaseMethod";

            var baseMethod = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

            var instance = Activator.CreateInstance(type);

            baseMethod.Invoke(instance, new object[] { });

            Tracker.LastValue.Should().Be("default");
        }
    }
}
