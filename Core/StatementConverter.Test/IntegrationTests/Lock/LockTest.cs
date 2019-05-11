using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class LockTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Setup();
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void Test()
        {
            var lamdaExpression = Helper.GetLamdaExpression("LockTestClass", "Test");

            var del = lamdaExpression.Compile();


            var thread1 = new Thread(() => del.DynamicInvoke());
            var thread2 = new Thread(() => del.DynamicInvoke());
            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Tracker.LastValue.Should().Be(2000000);
        }
    }
}
