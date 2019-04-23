using FluentAssertions;
using HotReloading.Test.TestCodes;
using NUnit.Framework;

namespace HotReloading.Test
{
    [TestFixture]
    public class PublicMethodTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Setup();
        }

        [Test]
        public void UpdateStaticMethodWithoutChanges()
        {
            PublicMethodTestClass.UpdateStaticMethod();

            Tracker.LastValue.Should().Be("default");
        }

        [Test]
        public void UpdateStaticMethodWithChanges()
        {
            var method = Helper.GetMethod("PublicMethodTestClass", "UpdateStaticMethod");

            Runtime.HandleCodeChange(new Core.CodeChange
            {
                Methods = new System.Collections.Generic.List<Core.Method>
                {
                    method
                }
            });

            PublicMethodTestClass.UpdateStaticMethod();

            Tracker.LastValue.Should().Be("change");
        }

        [Test]
        public void AddedStaticMethodAndCalledFromSameClass()
        {
            var existingMethod = Helper.GetMethod("PublicMethodTestClass", "AddedStaticMethodAndCalledFromSameClass1");
            var newMethod = Helper.GetMethod("PublicMethodTestClass", "AddedStaticMethodAndCalledFromSameClass2");

            Runtime.HandleCodeChange(new Core.CodeChange
            {
                Methods = new System.Collections.Generic.List<Core.Method>
                {
                    newMethod, existingMethod
                }
            });

            PublicMethodTestClass.AddedStaticMethodAndCalledFromSameClass1();

            Tracker.LastValue.Should().Be("change");
        }

        [Test]
        public void AddedStaticMethodAndCalledFromAnotherClass()
        {
            var method = Helper.GetMethod("PublicMethodTestClass1", "AddedStaticMethodAndCalledFromAnotherClass");

            Runtime.HandleCodeChange(new Core.CodeChange
            {
                Methods = new System.Collections.Generic.List<Core.Method>
                {
                    method
                }
            });

            PublicMethodTestClass1.AddedStaticMethodAndCalledFromAnotherClass();

            Tracker.LastValue.Should().Be("default");
        }

        [Test]
        public void UpdateInstanceMethod()
        {
            var method = Helper.GetMethod("PublicMethodTestClass", "UpdateInstanceMethod");

            Runtime.HandleCodeChange(new Core.CodeChange
            {
                Methods = new System.Collections.Generic.List<Core.Method>
                {
                    method
                }
            });

            var instance = new PublicMethodTestClass();

            instance.UpdateInstanceMethod();

            Tracker.LastValue.Should().Be("change");
        }

        [Test]
        public void AddedInstanceMethodAndCalledFromSameClass()
        {
            var existingMethod = Helper.GetMethod("PublicMethodTestClass", "AddedInstanceMethodAndCalledFromSameClass");
            var newMethod = Helper.GetMethod("PublicMethodTestClass", "AddedInstanceMethodAndCalledFromSameClass1");

            Runtime.HandleCodeChange(new Core.CodeChange
            {
                Methods = new System.Collections.Generic.List<Core.Method>
                {
                    existingMethod, newMethod
                }
            });

            var instance = new PublicMethodTestClass();

            instance.AddedInstanceMethodAndCalledFromSameClass();

            Tracker.LastValue.Should().Be("change");
        }

        [Test]
        public void AddedStaticMethodAndCalledFromInstanceMethod()
        {
            var instanceMethod = Helper.GetMethod("PublicMethodTestClass", "AddedStaticMethodAndCalledFromInstanceMethod");
            var newMethod = Helper.GetMethod("PublicMethodTestClass", "AddedStaticMethodAndCalledFromInstanceMethod1");

            Runtime.HandleCodeChange(new Core.CodeChange
            {
                Methods = new System.Collections.Generic.List<Core.Method>
                {
                    instanceMethod, newMethod
                }
            });

            var instance = new PublicMethodTestClass();

            instance.AddedStaticMethodAndCalledFromInstanceMethod();

            Tracker.LastValue.Should().Be("change");
        }

        [Test]
        public void AddedInstanceMethodAndCalledFromAnotherClass()
        {
            var method = Helper.GetMethod("PublicMethodTestClass1", "AddedInstanceMethodAndCalledFromAnotherClass");

            Runtime.HandleCodeChange(new Core.CodeChange
            {
                Methods = new System.Collections.Generic.List<Core.Method>
                {
                    method
                }
            });

            var instance = new PublicMethodTestClass1();

            instance.AddedInstanceMethodAndCalledFromAnotherClass();

            Tracker.LastValue.Should().Be("default");
        }

        [Test]
        public void MethodOverload1()
        {
            var method1 = Helper.GetMethod("PublicMethodTestClass", HotReloading.Runtime.GetMethodKey("MethodOverload"));
            var method2 = Helper.GetMethod("PublicMethodTestClass",  HotReloading.Runtime.GetMethodKey("MethodOverload", typeof(string).FullName));

            Runtime.HandleCodeChange(new Core.CodeChange
            {
                Methods = new System.Collections.Generic.List<Core.Method>
                {
                    method1, method2
                }
            });

            PublicMethodTestClass.MethodOverload();

            Tracker.LastValue.Should().Be("overload1");
        }

        [Test]
        public void MethodOverload2()
        {
            var method1 = Helper.GetMethod("PublicMethodTestClass", HotReloading.Runtime.GetMethodKey("MethodOverload"));
            var method2 = Helper.GetMethod("PublicMethodTestClass", HotReloading.Runtime.GetMethodKey("MethodOverload", typeof(string).FullName));

            Runtime.HandleCodeChange(new Core.CodeChange
            {
                Methods = new System.Collections.Generic.List<Core.Method>
                {
                    method1, method2
                }
            });

            PublicMethodTestClass.MethodOverload("test");

            Tracker.LastValue.Should().Be("overload2");
        }
    }
}
