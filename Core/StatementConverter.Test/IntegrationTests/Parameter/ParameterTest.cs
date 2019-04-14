using System;
using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class ParameterTest
    {
        [SetUp]
        public void Setup()
        {
            Tracker.Reset();
        }

        [Test]
        public void WhenAssignParameterToLocalVariable()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ParameterTestClass", "MethodWithParameter");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke("hello");

            Tracker.LastValue.Should().Be("hello");
        }
    }
}
