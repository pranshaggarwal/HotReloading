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

            var parameters = new List<Parameter>();
            SetupCodeChangeDelegate(type, @delegate, methodName, parameters);

            var instance2 = Activator.CreateInstance(type);

            baseMethod.Invoke(instance2, new object[] { });

            Tracker.LastValue.Should().Be("new");

            methodName = "BaseMethod1";

            var baseMethod1 = type.GetMethod(methodName);

            var instance3 = Activator.CreateInstance(type);

            var result = baseMethod1.Invoke(instance3, new string[] { "default" });

            result.Should().Be("default");

            parameters = new List<Parameter>()
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

            var parameters = new List<Parameter>()
            {
                new Parameter
                {
                    Type = new GenericHrType
                    {
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

            var parameters = new List<Parameter>()
            {
                new Parameter
                {
                    Type = new GenericHrType
                    {
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
            var assemblyToTest = "WrapOverrideMethodGenericClassDefinedType";
            string newAssemblyPath = Helper.GetInjectedAssembly(assemblyToTest);

            var assembly = Assembly.LoadFrom(newAssemblyPath);

            var type = assembly.GetType(Helper.GetFullClassname(assemblyToTest));

            var methodName = "BaseMethod";

            var baseMethod = type.GetMethod(methodName);

            var instance = Activator.CreateInstance(type);

            var result = baseMethod.Invoke(instance, new string[] { "default" });

            result.Should().Be("default");

            Func<object, string, string> @delegate = (object obj, string str) =>
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

            result = baseMethod.Invoke(instance2, new string[] { "default" });

            result.Should().Be("default1");
        }

        [Test]
        public void Test_WrapOverrideMethodWithGenericNested()
        {
            var assemblyToTest = "WrapOverrideMethodWithGenericNested";
            string newAssemblyPath = Helper.GetInjectedAssembly(assemblyToTest);

            var assembly = Assembly.LoadFrom(newAssemblyPath);

            var type = assembly.GetType(Helper.GetFullClassname(assemblyToTest));

            var methodName = "BaseMethod";

            var baseMethod = type.GetMethod(methodName);

            baseMethod = baseMethod.MakeGenericMethod(typeof(string));

            var instance = Activator.CreateInstance(type);

            Func<string> inputDelegate = () => "default";

            var result = baseMethod.Invoke(instance, new Func<string>[] { inputDelegate });

            result.Should().Be("default");

            Func<object, Func<string>, string> @delegate = (object obj, Func<string> str) =>
            {
                return str() + 1;
            };

            var parameters = new List<Parameter>()
            {
                new Parameter
                {
                    Type = new GenericHrType
                    {
                        Name = "System.Func`1<T>"
                    }
                }
            };
            SetupCodeChangeDelegate(type, @delegate, methodName, parameters);

            var instance2 = Activator.CreateInstance(type);

            result = baseMethod.Invoke(instance2, new Func<string>[] { inputDelegate });

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
            if (RuntimeMemory.Methods.ContainsKey(type))
                RuntimeMemory.Methods[type] = methods;
            else
                RuntimeMemory.Methods.Add(type, methods);
        }
    }
}
