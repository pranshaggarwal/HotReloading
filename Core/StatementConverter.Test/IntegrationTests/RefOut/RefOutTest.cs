using System;
using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class RefOutTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Setup();
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void PassRefParameterToMethod()
        {
            var lamdaExpression = Helper.GetLamdaExpression("RefOutTestClass", "PassRefParameterToMethod");

            var del = lamdaExpression.Compile();

            var instance = new ExpressionTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void PassOutParameterToMethod()
        {
            var lamdaExpression = Helper.GetLamdaExpression("RefOutTestClass", "PassRefParameterToMethod");

            var del = lamdaExpression.Compile();

            var instance = new ExpressionTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void DefineRefMethod()
        {
            var lamdaExpression = Helper.GetLamdaExpression("RefOutTestClass", "DefineRefMethod");

            var del = lamdaExpression.Compile();

            var instance = new ExpressionTestClass();

            string value = "";

            throw new NotImplementedException();

            //del.DynamicInvoke(instance, ref value);

            value.Should().Be("hello");
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void DefineOutMethod()
        {
            var lamdaExpression = Helper.GetLamdaExpression("RefOutTestClass", "DefineOutMethod");

            var del = lamdaExpression.Compile();

            var instance = new ExpressionTestClass();

            string value = "";

            throw new NotImplementedException();

            //del.DynamicInvoke(instance, out value);

            value.Should().Be("value");
        }
    }
}
