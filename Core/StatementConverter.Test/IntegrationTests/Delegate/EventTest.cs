using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class EventTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Setup();
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void AddEventHandler1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("EventTestClass", "AddEventHandler1");

            var del = lamdaExpression.Compile();

            var instance = new ExpressionTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void AddEventHandler2()
        {
            var lamdaExpression = Helper.GetLamdaExpression("EventTestClass", "AddEventHandler2");

            var del = lamdaExpression.Compile();

            var instance = new ExpressionTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void AddEventHandler3()
        {
            var lamdaExpression = Helper.GetLamdaExpression("EventTestClass", "AddEventHandler3");

            var del = lamdaExpression.Compile();

            var instance = new ExpressionTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void RemoveEventHandler4()
        {
            var lamdaExpression = Helper.GetLamdaExpression("EventTestClass", "RemoveEventHandler4");

            var del = lamdaExpression.Compile();

            var instance = new ExpressionTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }
    }
}
