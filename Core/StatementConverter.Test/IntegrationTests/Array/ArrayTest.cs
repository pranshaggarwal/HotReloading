using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class ArrayTest
    {
        [SetUp]
        public void Setup()
        {
            Tracker.Reset();
        }

        [Test]
        public void InitOneDArrayWithoutContent()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ArrayTestClass", "InitOneDArrayWithoutContent");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(5);
        }

        [Test]
        public void InitTwoDArrayWithoutContent()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ArrayTestClass", "InitTwoDArrayWithoutContent");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(15);
        }

        [Test]
        public void InitOneDArrayWithContent()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ArrayTestClass", "InitOneDArrayWithContent");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(3);
        }

        [Test]
        public void InitTwoDArrayWithContent()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ArrayTestClass", "InitTwoDArrayWithContent");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(6);
        }

        [Test]
        public void InitOneDArrayWithArrayInitializer()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ArrayTestClass", "InitOneDArrayWithArrayInitializer");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(3);
        }

        [Test]
        public void InitTwoDArrayWithArrayInitializer()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ArrayTestClass", "InitTwoDArrayWithArrayInitializer");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(6);
        }

        [Test]
        public void GetOneDArrayValue()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ArrayTestClass", "GetOneDArrayValue");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(2);
        }

        [Test]
        public void GetTwoDArrayValue()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ArrayTestClass", "GetTwoDArrayValue");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(5);
        }

        [Test]
        public void SetOneDArrayValue()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ArrayTestClass", "SetOneDArrayValue");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(1);
        }

        [Test]
        public void SetTwoDArrayValue()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ArrayTestClass", "SetTwoDArrayValue");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(2);
        }
    }
}
