using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class DelegateTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Setup();
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void DefineDelegate1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("DelegateTestClass", "DefineDelegate1");

            var del = lamdaExpression.Compile();

            var instance = new DelegateTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void DefineDelegate2()
        {
            var lamdaExpression = Helper.GetLamdaExpression("DelegateTestClass", "DefineDelegate2");

            var del = lamdaExpression.Compile();

            var instance = new DelegateTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void DefineDelegate3()
        {
            var lamdaExpression = Helper.GetLamdaExpression("DelegateTestClass", "DefineDelegate3");

            var del = lamdaExpression.Compile();

            var instance = new DelegateTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void DefineDelegate4()
        {
            var lamdaExpression = Helper.GetLamdaExpression("DelegateTestClass", "DefineDelegate4");

            var del = lamdaExpression.Compile();

            var instance = new DelegateTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void DefineDelegate5()
        {
            var lamdaExpression = Helper.GetLamdaExpression("DelegateTestClass", "DefineDelegate5");

            var del = lamdaExpression.Compile();

            var instance = new DelegateTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void DefineDelegate6()
        {
            var lamdaExpression = Helper.GetLamdaExpression("DelegateTestClass", "DefineDelegate6");

            var del = lamdaExpression.Compile();

            var instance = new DelegateTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Ignore("Ignoring for now")]
        [Test]
        public async Task DefineDelegate7()
        {
            var lamdaExpression = Helper.GetLamdaExpression("DelegateTestClass", "DefineDelegate7");

            var del = lamdaExpression.Compile();

            var instance = new DelegateTestClass();

            await (Task)del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void DefineDelegate8()
        {
            var lamdaExpression = Helper.GetLamdaExpression("DelegateTestClass", "DefineDelegate8");

            var del = lamdaExpression.Compile();

            var instance = new DelegateTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void AddDelegate()
        {
            var lamdaExpression = Helper.GetLamdaExpression("DelegateTestClass", "AddDelegate");

            var del = lamdaExpression.Compile();

            var instance = new DelegateTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be(2);
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void RemoveDelegate()
        {
            var lamdaExpression = Helper.GetLamdaExpression("DelegateTestClass", "RemoveDelegate");

            var del = lamdaExpression.Compile();

            var instance = new DelegateTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void PassDelegateToMethod1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("DelegateTestClass", "PassDelegateToMethod1");

            var del = lamdaExpression.Compile();

            var instance = new DelegateTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Ignore("Ignoring for now")]
        [Test]
        public void PassDelegateToMethod2()
        {
            var lamdaExpression = Helper.GetLamdaExpression("DelegateTestClass", "PassDelegateToMethod2");

            var del = lamdaExpression.Compile();

            var instance = new DelegateTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }
    }


}
