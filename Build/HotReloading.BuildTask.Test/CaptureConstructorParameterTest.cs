using System;
using System.Collections;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;

namespace HotReloading.BuildTask.Test
{
    [TestFixture]
    public class CaptureConstructorParameterTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Reset();
        }

        [Test]
        public void Test_Constructor_WithoutParameter()
        {
            var assemblyToTest = "CaptureConstructorParameter";
            string newAssemblyPath = Helper.GetInjectedAssembly(assemblyToTest);

            var assembly = Assembly.LoadFrom(newAssemblyPath);

            var type = assembly.GetType(Helper.GetFullClassname(assemblyToTest));

            var defaultConstructor = type.GetConstructor(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { }, null);

            var obj = defaultConstructor.Invoke(new object[] { });

            var parametersFields = type.GetField("hotReloading_Ctor_Parameters", BindingFlags.Instance | BindingFlags.NonPublic);
            var parameters = parametersFields.GetValue(obj) as ArrayList;
            parameters.Count.Should().Be(0);
        }

        [Test]
        public void Test_Constructor_WithParameter()
        {
            var assemblyToTest = "CaptureConstructorParameter";
            string newAssemblyPath = Helper.GetInjectedAssembly(assemblyToTest);

            var assembly = Assembly.LoadFrom(newAssemblyPath);

            var type = assembly.GetType(Helper.GetFullClassname(assemblyToTest));

            var defaultConstructor = type.GetConstructor(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { typeof(string) }, null);

            var obj = defaultConstructor.Invoke(new object[] { "test" });

            var parametersFields = type.GetField("hotReloading_Ctor_Parameters", BindingFlags.Instance | BindingFlags.NonPublic);
            var parameters = parametersFields.GetValue(obj) as ArrayList;
            parameters[0].Should().Be("test");
        }

        [Test]
        public void Test_Constructor_With2Parameter()
        {
            var assemblyToTest = "CaptureConstructorParameter";
            string newAssemblyPath = Helper.GetInjectedAssembly(assemblyToTest);

            var assembly = Assembly.LoadFrom(newAssemblyPath);

            var type = assembly.GetType(Helper.GetFullClassname(assemblyToTest));

            var defaultConstructor = type.GetConstructor(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { typeof(string), typeof(string)  }, null);

            var obj = defaultConstructor.Invoke(new object[] { "test1", "test2" });

            var parametersFields = type.GetField("hotReloading_Ctor_Parameters", BindingFlags.Instance | BindingFlags.NonPublic);
            var parameters = parametersFields.GetValue(obj) as ArrayList;
            parameters[0].Should().Be("test1");
            parameters[1].Should().Be("test2");
        }
    }
}
