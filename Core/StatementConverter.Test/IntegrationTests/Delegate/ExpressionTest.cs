using System;
using System.Linq.Expressions;
using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class ExpressionTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Setup();
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void AssignExpression()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ExpressionTestClass", "AssignExpression");

            var del = lamdaExpression.Compile();

            var instance = new ExpressionTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void PassExpressionInMethod()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ExpressionTestClass", "PassExpressionInMethod");

            var del = lamdaExpression.Compile();

            var instance = new ExpressionTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        private void TestFunction(Expression<Action> expression)
        {
            expression.Compile()();
        }
    }
}
