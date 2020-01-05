using System;
using System.Collections.Generic;
using System.Reflection;
using BaseAssembly;
using FluentAssertions;
using HotReloading.Core;
using Moq;
using NUnit.Framework;
using HotReloading.Syntax;

namespace HotReloading.BuildTask.Test
{
    [TestFixture]
    public class WrapInstanceMethodTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Reset();
        }

        [Test]
        public void Test_WrapInstanceMethod_WithoutParameter()
        {
            var assemblyToTest = "WrapInstanceMethodWithoutParameter";
            string newAssemblyPath = Helper.GetInjectedAssembly(assemblyToTest);

            var assembly = Assembly.LoadFrom(newAssemblyPath);

            var type = assembly.GetType(Helper.GetFullClassname(assemblyToTest));

            string methodName = "TestMethod";

            var instance = Activator.CreateInstance(type);

            var testMethod = type.GetMethod(methodName);

            testMethod.Invoke(instance, new object[] { });

            Tracker.LastValue.Should().Be("default");

            Action<object> @delegate = (object obj) =>
            {
                Tracker.Call("new");
            };

            var parameters = new List<Parameter>();
            SetupCodeChangeDelegate(type, @delegate, methodName, parameters);

            var instance2 = Activator.CreateInstance(type);

            testMethod.Invoke(instance2, new object[] { });

            Tracker.LastValue.Should().Be("new");
        }

        [Test]
        public void Test_WrapInstanceMethod_WithParameter()
        {
            var assemblyToTest = "WrapInstanceMethodWithParameter";
            string newAssemblyPath = Helper.GetInjectedAssembly(assemblyToTest);

            var assembly = Assembly.LoadFrom(newAssemblyPath);

            var type = assembly.GetType(Helper.GetFullClassname(assemblyToTest));

            string methodName = "TestMethod";

            var testMethod = type.GetMethod(methodName);

            var instance = Activator.CreateInstance(type);

            var result = testMethod.Invoke(instance, new object[] { "default" });

            result.Should().Be("default");

            Func<object, string, string> @delegate = (obj, str) =>
            {
                return str + 1;
            };

            var parameters = new List<Parameter>()
            {
                new Parameter
                {
                    Type = new HrType
                    {
                        Name = typeof(string).FullName,
                        AssemblyName = typeof(string).Assembly.GetName().Name
                    }
                }
            };

            SetupCodeChangeDelegate(type, @delegate, methodName, parameters);

            var instance2 = Activator.CreateInstance(type);

            result = testMethod.Invoke(instance2, new string[] { "default" });

            result.Should().Be("default1");
        }

        private static void SetupCodeChangeDelegate(System.Type type, Delegate @delegate, string methodName, List<Parameter> parameters)
        {
            var methodContainer = new Mock<IMethodContainer>();

            methodContainer.Setup(x => x.GetDelegate()).Returns(@delegate);
            methodContainer.SetupGet(x => x.Method).Returns(new Syntax.Method
            {
                Name = methodName,
                Parameters = parameters
            });
            var methods = new List<IMethodContainer>
            {
                methodContainer.Object
            };
            RuntimeMemory.Methods.Add(type, methods);
        }
    }
}
