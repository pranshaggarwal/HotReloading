using System;
using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class GenericMethodTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Setup();
        }

        [Test]
        public void Default()
        {
            var lamdaExpression = Helper.GetLamdaExpression("GenericMethodTestClass", "GenericMethod");

            var del = lamdaExpression.Compile();

            var instance = new GenericMethodTestClass();

            var result = del.DynamicInvoke(instance, "hello");

            result.Should().Be("hello");
        }
    }
}
