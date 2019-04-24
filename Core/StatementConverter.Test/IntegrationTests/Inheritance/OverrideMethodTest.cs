using System;
using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class OverrideMethodTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Setup();
        }

        [Test]
        public void BaseMethod()
        {
            var lamdaExpression = Helper.GetLamdaExpression("OverrideMethodTestClass", "BaseMethod");

            var del = lamdaExpression.Compile();

            var instance = new OverrideMethodTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void BaseMethod1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("OverrideMethodTestClass", "BaseMethod1");

            var del = lamdaExpression.Compile();

            var instance = new OverrideMethodTestClass();

            var result = del.DynamicInvoke(instance, "hello");

            result.Should().Be("hello");
        }
    }
}
