using System;
using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class UsingTest
    {
        [SetUp]
        public void Setup()
        {
            Tracker.Reset();
        }

        [Test]
        public void TestUsing1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("UsingTestClass", "TestUsing1");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();
            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void TestUsing2()
        {

            var lamdaExpression = Helper.GetLamdaExpression("UsingTestClass", "TestUsing2");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();
            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void TestUsing3()
        {
            var lamdaExpression = Helper.GetLamdaExpression("UsingTestClass", "TestUsing3");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();
            Tracker.LastValue.Should().Be("hello");
        }
    }
}
