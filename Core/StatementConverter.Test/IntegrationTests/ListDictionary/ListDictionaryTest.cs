using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class ListDictionaryTest
    {
        [SetUp]
        public void Setup()
        {
            Tracker.Reset();
        }

        [Test]
        public void InitList()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ListDictionaryTestClass", "InitList");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();
            Tracker.LastValue.Should().Be(2);
        }

        [Test]
        public void InitDictionary()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ListDictionaryTestClass", "InitDictionary");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void InitListWithListInitializer()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ListDictionaryTestClass", "InitListWithListInitializer");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(2);
        }

        [Test]
        public void InitDicWithDicInitializer()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ListDictionaryTestClass", "InitDicWithDicInitializer");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void InitListWithListInitializerWithoutParanthesis()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ListDictionaryTestClass", "InitListWithListInitializerWithoutParanthesis");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(2);
        }

        [Test]
        public void InitDicWithDicInitializerWithoutParanthesis()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ListDictionaryTestClass", "InitDicWithDicInitializerWithoutParanthesis");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }
    }
}
