using System;
using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class AssignmentTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Setup();
        }
        
        [Test]
        public void WhenLocalVariableAssign()
        {
            var lamdaExpression = Helper.GetLamdaExpression("AssignmentTestClass", "AssignLocalVariable");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void WhenValueCopyFromOneLocalVariableToOther()
        {
            var lamdaExpression = Helper.GetLamdaExpression("AssignmentTestClass", "AssignLocalVariableFromOtherLocalVariable");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void WhenAssignParameterToLocalVariable()
        {
            var lamdaExpression = Helper.GetLamdaExpression("AssignmentTestClass", "AssignParameterToLocalVariable");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke("hello");

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void WhenFieldIsAssign()
        {
            var lamdaExpression = Helper.GetLamdaExpression("AssignmentTestClass", "AssignField");

            var del = lamdaExpression.Compile();

            var instance = new AssignmentTestClass();

            del.DynamicInvoke(instance);

            instance.field.Should().Be("hello");
        }

        [Test]
        public void WhenPropertyIsAssign()
        {
            var lamdaExpression = Helper.GetLamdaExpression("AssignmentTestClass", "AssignProperty");

            var del = lamdaExpression.Compile();

            var instance = new AssignmentTestClass();

            del.DynamicInvoke(instance);

            instance.Property.Should().Be("hello");
        }

        [Test]
        public void WhenAssignInstancePropertyOfOtherClass()
        {
            var lamdaExpression = Helper.GetLamdaExpression("AssignmentTestClass", "AssignInstancePropertyOfOtherClass");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void WhenAssignStaticPropertyOfOtherClass()
        {
            var lamdaExpression = Helper.GetLamdaExpression("AssignmentTestClass", "AssignStaticPropertyOfOtherClass");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }
    }
}
