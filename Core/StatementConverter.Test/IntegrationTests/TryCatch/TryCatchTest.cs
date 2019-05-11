using System;
using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class TryCatchTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Setup();
        }

        [Test]
        public void NoException()
        {
            var lamdaExpression = Helper.GetLamdaExpression("TryCatchTestClass", "NoException");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void Exception()
        {
            var lamdaExpression = Helper.GetLamdaExpression("TryCatchTestClass", "Exception");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void MultipleCatchStatement()
        {
            var lamdaExpression = Helper.GetLamdaExpression("TryCatchTestClass", "MultipleCatchStatement");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void FinallyBlockWithNoException()
        {
            var lamdaExpression = Helper.GetLamdaExpression("TryCatchTestClass", "FinallyBlockWithNoException");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void FinallyBlockWithException()
        {
            var lamdaExpression = Helper.GetLamdaExpression("TryCatchTestClass", "FinallyBlockWithException");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }
    }
}
