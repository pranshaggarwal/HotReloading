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

            CodeChangeHandler.HandleCodeChange(new Core.CodeChange
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

            CodeChangeHandler.HandleCodeChange(new Core.CodeChange
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

            CodeChangeHandler.HandleCodeChange(new Core.CodeChange
            {
                Methods = new System.Collections.Generic.List<Core.Method>
                {
                    method
                }
            });

            PublicMethodTestClass1.AddedStaticMethodAndCalledFromAnotherClass();

            Tracker.LastValue.Should().Be("default");
        }

        public void UpdateInstanceMethod()
        {

        }

        public void AddedInstanceMethodAndCalledFromSameClass()
        {

        }

        public void AddedInstanceMethodAndCalledFromAnotherClass()
        {

        }

        public void MethodOverload()
        {

        }
    }
}
