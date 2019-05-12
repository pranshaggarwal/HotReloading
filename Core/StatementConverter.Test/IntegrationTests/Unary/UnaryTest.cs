using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class UnaryTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Setup();
        }

        [Test]
        public void NegativeNumber()
        {
            var lamdaExpression = Helper.GetLamdaExpression("UnaryTestClass", "NegativeNumber");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(-5);
        }

        [Test]
        public void PositiveNumber()
        {
            var lamdaExpression = Helper.GetLamdaExpression("UnaryTestClass", "PositiveNumber");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(5);
        }

        [Test]
        public void Decrement()
        {
            var lamdaExpression = Helper.GetLamdaExpression("UnaryTestClass", "Decrement");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(4);
        }

        [Test]
        public void Increment()
        {
            var lamdaExpression = Helper.GetLamdaExpression("UnaryTestClass", "Increment");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(6);
        }

        [Test]
        public void PostDecrement()
        {
            var lamdaExpression = Helper.GetLamdaExpression("UnaryTestClass", "PostDecrement");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(5);
        }

        [Test]
        public void PostIncrement()
        {
            var lamdaExpression = Helper.GetLamdaExpression("UnaryTestClass", "PostIncrement");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(5);
        }

        [Test]
        public void PreDecrement()
        {
            var lamdaExpression = Helper.GetLamdaExpression("UnaryTestClass", "PreDecrement");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(4);
        }

        [Test]
        public void PreIncrement()
        {
            var lamdaExpression = Helper.GetLamdaExpression("UnaryTestClass", "PreIncrement");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(6);
        }

        [Test]
        public void Convert()
        {
            var lamdaExpression = Helper.GetLamdaExpression("UnaryTestClass", "Convert");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(5);
        }

        [Test]
        public void Not()
        {
            var lamdaExpression = Helper.GetLamdaExpression("UnaryTestClass", "Not");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(true);
        }

        [Test]
        public void OnesComplement()
        {
            var lamdaExpression = Helper.GetLamdaExpression("UnaryTestClass", "OnesComplement");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(~0b00110011);
        }
    }
}
