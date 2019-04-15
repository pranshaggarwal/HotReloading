using System;
using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class LoopTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Setup();
        }
        
        [Test]
        public void TestWhile()
        {
            var lamdaExpression = Helper.GetLamdaExpression("LoopTestClass", "TestWhile");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();
            Tracker.LastValue.Should().Be(5);
        }

        [Test]
        public void TestDoWhile()
        {
            var lamdaExpression = Helper.GetLamdaExpression("LoopTestClass", "TestDoWhile");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();
            Tracker.LastValue.Should().Be(5);
        }

        [Test]
        public void TestFor1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("LoopTestClass", "TestFor1");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();
            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void TestFor2()
        {
            var lamdaExpression = Helper.GetLamdaExpression("LoopTestClass", "TestFor2");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();
            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void TestFor3()
        {
            var lamdaExpression = Helper.GetLamdaExpression("LoopTestClass", "TestFor3");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();
            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void TestFor4()
        {
            var lamdaExpression = Helper.GetLamdaExpression("LoopTestClass", "TestFor4");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();
            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void TestFor5()
        {
            var lamdaExpression = Helper.GetLamdaExpression("LoopTestClass", "TestFor5");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();
            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void TestFor6()
        {
            var lamdaExpression = Helper.GetLamdaExpression("LoopTestClass", "TestFor6");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();
            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void TestForEach()
        {
            var lamdaExpression = Helper.GetLamdaExpression("LoopTestClass", "TestForEach");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();
            Tracker.LastValue.Should().Be("hello");
        }
    }
}
