using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class ConditionalTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Setup();
        }
        
        [Test]
        public void IfWithoutBlock()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ConditionalTestClass", "IfWithoutBlock");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void IfWithBlock()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ConditionalTestClass", "IfWithBlock");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void IfElseBlock()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ConditionalTestClass", "IfElseBlock");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void IfElseIfBlock()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ConditionalTestClass", "IfElseIfBlock");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void IfElseIfBlock1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ConditionalTestClass", "IfElseIfBlock1");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        [Ignore("")]
        public void IfIsPattern()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ConditionalTestClass", "IfIsPattern");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void ConditionalStatement()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ConditionalTestClass", "ConditionalStatement");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void SwitchStatment()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ConditionalTestClass", "SwitchStatment");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void SwitchWithMultipleCases()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ConditionalTestClass", "SwitchWithMultipleCases");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void SwitchWithBlock()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ConditionalTestClass", "SwitchWithBlock");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void SwitchDefault()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ConditionalTestClass", "SwitchDefault");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        [Ignore("Ignore")]
        public void SwitchPattern()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ConditionalTestClass", "SwitchPattern");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        [Ignore("Ignore")]
        public void SwithcPattern2()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ConditionalTestClass", "SwithcPattern2");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }
    }

}
