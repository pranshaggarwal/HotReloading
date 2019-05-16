using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class StringTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Setup();
        }

        [Test]
        public void StringInterpolation()
        {
            var lamdaExpression = Helper.GetLamdaExpression("StringTestClass", "StringInterpolation");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("1hello1");
        }
    }
}
