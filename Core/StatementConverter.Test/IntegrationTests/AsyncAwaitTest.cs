using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class AsyncAwaitTest
    {
        [SetUp]
        public void Setup()
        {
            Tracker.Reset();
        }

        [Test]
        public async Task TestAwaitTaskCall()
        {
            var lamdaExpression = Helper.GetLamdaExpression("AsyncAwaitTestClass", "TestAwaitTaskCall");

            var del = lamdaExpression.Compile();

            var instance = new AsyncAwaitTestClass();

            del.DynamicInvoke(instance);

            await Task.Delay(3);

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public async Task TestAwaitTaskStringCall()
        {
            var lamdaExpression = Helper.GetLamdaExpression("AsyncAwaitTestClass", "TestAwaitTaskStringCall");

            var del = lamdaExpression.Compile();

            var instance = new AsyncAwaitTestClass();

            del.DynamicInvoke(instance);

            await Task.Delay(3);

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public async Task TestAwaitReturnStringCall()
        {
            var lamdaExpression = Helper.GetLamdaExpression("AsyncAwaitTestClass", "TestAwaitReturnStringCall");

            var del = lamdaExpression.Compile();

            var instance = new AsyncAwaitTestClass();

            var retValue = await (Task<string>)del.DynamicInvoke(instance);

            retValue.Should().Be("hello");
        }
    }
}
