using System;
using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class ObjectCreationTest
    {
        [SetUp]
        public void Setup()
        {
            Tracker.Reset();
        }

        [Test]
        public void WhenObjectCreated()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ObjectCreationTestClass", "CreateNewObject");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.MethodCalled.Should().BeTrue();
        }

        [Test]
        public void CreateNewObjectWithParameter()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ObjectCreationTestClass", "CreateNewObjectWithParameter");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void CreateObjectWithInitializer()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ObjectCreationTestClass", "CreateObjectWithInitializer");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("Hello");
        }



        [Test]
        public void CreateObjectWithInitializer1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ObjectCreationTestClass", "CreateObjectWithInitializer1");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("Hello");
        }

        [Test]
        public void CreateObjectWithInitializerWithoutParanthesis()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ObjectCreationTestClass", "CreateObjectWithInitializerWithoutParanthesis");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("Hello");
        }

        [Test]
        public void PassNewObjectToMethod()
        {
            var lamdaExpression = Helper.GetLamdaExpression("ObjectCreationTestClass", "PassNewObjectToMethod");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("Hello");
        }
    }
}
