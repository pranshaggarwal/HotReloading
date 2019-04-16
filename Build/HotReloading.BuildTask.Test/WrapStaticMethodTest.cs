using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BaseAssembly;
using FluentAssertions;
using Moq;
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

            string methodName = "TestMethod";

            var testMethod = type.GetMethod(methodName);

            testMethod.Invoke(null, new object[] { });

            Tracker.LastValue.Should().Be("default");

            Action @delegate = () =>
            {
                Tracker.Call("new");
            };

            var parameters = new List<Core.Parameter>();
            SetupCodeChangeDelegate(type, @delegate, methodName, parameters);

            testMethod.Invoke(null, new object[] { });

            Tracker.LastValue.Should().Be("new");

            File.Delete(newAssemblyPath);
        }


        private static void SetupCodeChangeDelegate(Type type, Delegate @delegate, string methodName, List<Core.Parameter> parameters)
        {
            var methodContainer = new Mock<IMethodContainer>();

            methodContainer.Setup(x => x.GetDelegate()).Returns(@delegate);
            methodContainer.SetupGet(x => x.Method).Returns(new HotReloading.Core.Method
            {
                Name = methodName,
                Parameters = parameters
            });
            var methods = new List<IMethodContainer>
            {
                methodContainer.Object
            };
            CodeChangeHandler.Methods.Add(type, methods);
        }
    }
}
