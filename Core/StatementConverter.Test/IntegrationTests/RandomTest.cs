using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class RandomTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Setup();
        }
        
        [Test]
        public void Default()
        {
            var lamdaExpression = Helper.GetLamdaExpression("RandomTestClass", "Default");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(0);
        }

        [Test]
        public static void TypeOf()
        {
            var lamdaExpression = Helper.GetLamdaExpression("RandomTestClass", "TypeOf");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(typeof(string));
        }

        [Test]
        public static void NameOf()
        {
            var lamdaExpression = Helper.GetLamdaExpression("RandomTestClass", "NameOf");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void IsType()
        {
            var lamdaExpression = Helper.GetLamdaExpression("RandomTestClass", "IsType");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(true);
        }

        [Test]
        public void IsConst()
        {
            var lamdaExpression = Helper.GetLamdaExpression("RandomTestClass", "IsConst");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(true);
        }

        [Test]
        public void As()
        {
            var lamdaExpression = Helper.GetLamdaExpression("RandomTestClass", "As");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }
    }
}
