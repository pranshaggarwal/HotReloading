using System;
using System.Collections.Generic;
using System.Reflection;
using BaseAssembly;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace HotReloading.BuildTask.Test
{
    [TestFixture]
    public class WrapOverrideMethodTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Reset();
        }

        [Test]
        public void Test_WrapOverrideMethod()
        {
            var assemblyToTest = "WrapOverrideMethodTest";
            string newAssemblyPath = Helper.GetInjectedAssembly(assemblyToTest);

            var assembly = Assembly.LoadFrom(newAssemblyPath);

            var type = assembly.GetType(Helper.GetFullClassname(assemblyToTest));

            var methodName = "BaseMethod";

            var baseMethod = type.GetMethod(methodName);

            var instance = Activator.CreateInstance(type);

            baseMethod.Invoke(instance, new object[] { });

            Tracker.LastValue.Should().Be("default");

            Action<object> @delegate = (object obj) =>
            {
                Tracker.Call("new");
            };

            var parameters = new List<Core.Parameter>();
            SetupCodeChangeDelegate(type, @delegate, methodName, parameters);

            var instance2 = Activator.CreateInstance(type);

            baseMethod.Invoke(instance2, new object[] { });

            Tracker.LastValue.Should().Be("new");

            methodName = "BaseMethod1";

            var baseMethod1 = type.GetMethod(methodName);

            var instance3 = Activator.CreateInstance(type);

            var result = baseMethod1.Invoke(instance3, new string[] { "default" });

            result.Should().Be("default");

            parameters = new List<Core.Parameter>()
            {
                new Core.Parameter
                {
                    Type = new Core.ClassType
                    {
                        Name = typeof(string).FullName,
                        AssemblyName = typeof(string).Assembly.GetName().Name
                    }
                }
            };

            Func<object, string, string> @delegate1 = (obj, str) =>
            {
                return str + 1;
            };

            SetupCodeChangeDelegate(type, @delegate1, methodName, parameters);

            var instance4 = Activator.CreateInstance(type);

            result = baseMethod1.Invoke(instance4, new string[] { "default" });

            result.Should().Be("default1");
        }

        [Test]
        public void Test_WrapOverrideGenericMethod()
        {
            var assemblyToTest = "WrapOverrideGenericMethod";
            string newAssemblyPath = Helper.GetInjectedAssembly(assemblyToTest);

            var assembly = Assembly.LoadFrom(newAssemblyPath);

            var type = assembly.GetType(Helper.GetFullClassname(assemblyToTest));

            var methodName = "BaseMethod";

            var baseMethod = type.GetMethod(methodName);

            baseMethod = baseMethod.MakeGenericMethod(typeof(string));

            var instance = Activator.CreateInstance(type);

            var result = baseMethod.Invoke(instance, new string[] { "default" });

            result.Should().Be("default");

            Func<object, string, string> @delegate = (object obj, string str) =>
            {
                return str + 1;
            };

            var parameters = new List<Core.Parameter>()
            {
                new Core.Parameter
                {
                    Type = new Core.ClassType
                    {
                        IsGeneric = true,
                        Name = "T"
                    }
                }
            };
            SetupCodeChangeDelegate(type, @delegate, methodName, parameters);

            var instance2 = Activator.CreateInstance(type);

            result = baseMethod.Invoke(instance2, new string[] { "default" });

            result.Should().Be("default1");
        }

        [Test]
        public void Test_WrapOverrideMethodGenericClass()
        {
            var assemblyToTest = "WrapOverrideMethodGenericClass";
            string newAssemblyPath = Helper.GetInjectedAssembly(assemblyToTest);

            var assembly = Assembly.LoadFrom(newAssemblyPath);

            var type = assembly.GetType(Helper.GetFullClassname(assemblyToTest) + "`1");

            type = type.MakeGenericType(typeof(string));

            var methodName = "BaseMethod";

            var baseMethod = type.GetMethod(methodName);

            var instance = Activator.CreateInstance(type);

            var result = baseMethod.Invoke(instance, new string[] { "default" });

            result.Should().Be("default");

            Func<object, string, string> @delegate = (object obj, string str) =>
            {
                return str + 1;
            };

            var parameters = new List<Core.Parameter>()
            {
                new Core.Parameter
                {
                    Type = new Core.ClassType
                    {
                        IsGeneric = true,
                        Name = "T"
                    }
                }
            };
            SetupCodeChangeDelegate(type, @delegate, methodName, parameters);

            var instance2 = Activator.CreateInstance(type);

            result = baseMethod.Invoke(instance2, new string[] { "default" });

            result.Should().Be("default1");
        }

        [Test]
        public void Test_WrapOverrideMethodGenericClassDefinedType()
        {

        }

        [Test]
        public void Test_WrapOverrideMethodWithGenericNested()
        {

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
            if (CodeChangeHandler.Methods.ContainsKey(type))
                CodeChangeHandler.Methods[type] = methods;
            else
                CodeChangeHandler.Methods.Add(type, methods);
        }
    }
}
