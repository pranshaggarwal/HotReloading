using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            string methodName = "TestMethod";

            var testMethod = type.GetMethod(methodName);

            testMethod.Invoke(null, new object[] { });

            Tracker.LastValue.Should().Be("default");

            Action @delegate = () =>
            {
                Tracker.Call("new");
            };

            var parameters = new List<Parameter>();
            SetupCodeChangeDelegate(type, @delegate, methodName, parameters);

            testMethod.Invoke(null, new object[] { });

            Tracker.LastValue.Should().Be("new");
        }

        [Test]
        public void Test_WrapStaticMethod_WithParameter()
        {
            var assemblyToTest = "WrapStaticMethodWithParameter";
            string newAssemblyPath = Helper.GetInjectedAssembly(assemblyToTest);

            var assembly = Assembly.LoadFrom(newAssemblyPath);

            var type = assembly.GetType(Helper.GetFullClassname(assemblyToTest));

            string methodName = "TestMethod";

            var testMethod = type.GetMethod(methodName);

            var result = testMethod.Invoke(null, new object[] { "default" });

            result.Should().Be("default");

            Func<string, string> @delegate = (str) =>
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

            result = testMethod.Invoke(null, new string[] { "default" });

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
