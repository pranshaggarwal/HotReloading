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
            Tracker.Reset();
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

        public void AddedStaticMethodAndCalledFromSameClass()
        {

        }

        public void AddedStaticMethodAndCalledFromAnotherClass()
        {

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
