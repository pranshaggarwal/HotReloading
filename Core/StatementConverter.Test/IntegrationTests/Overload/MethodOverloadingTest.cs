using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class MethodOverloadingTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Setup();
        }

        [Test]
        public static void DifferentParameterLength()
        {
            var lamdaExpression = Helper.GetLamdaExpression("MethodOverloadingTestClass", "DifferentParameterLength");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public static void DifferentParameterType()
        {
            var lamdaExpression = Helper.GetLamdaExpression("MethodOverloadingTestClass", "DifferentParameterType");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public static void ImplicitParameterType()
        {
            var lamdaExpression = Helper.GetLamdaExpression("MethodOverloadingTestClass", "ImplicitParameterType");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public static void ImplicitParameterType1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("MethodOverloadingTestClass", "ImplicitParameterType1");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public static void OptionalParameter()
        {
            var lamdaExpression = Helper.GetLamdaExpression("MethodOverloadingTestClass", "OptionalParameter");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public static void NamedParameter()
        {
            var lamdaExpression = Helper.GetLamdaExpression("MethodOverloadingTestClass", "NamedParameter");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }
    }
}
