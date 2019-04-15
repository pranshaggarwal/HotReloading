using System;
using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class LocalVariableDeclarationTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Setup();
        }
        
        [Test]
        public void WhenLocalVariableDeclared_NoCompileOrRuntimeError()
        {
            var lamdaExpression = Helper.GetLamdaExpression("LocalVariableTestClass", "DeclareSingleLocalVariable");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.MethodCalled.Should().BeTrue();
        }

        [Test]
        public void WhenMultipleLocalVariableDeclaredInOneLine()
        {
            var lamdaExpression = Helper.GetLamdaExpression("LocalVariableTestClass", "DeclareMultipleLocalVariable");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.MethodCalled.Should().BeTrue();
        }

        [Test]
        public void WhenVariableDeclareAndAssign()
        {
            var lamdaExpression = Helper.GetLamdaExpression("LocalVariableTestClass", "DeclareAndAssignLocalVariable");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void WhenAssignParameterToLocalVariableWithDeclaration()
        {
            var lamdaExpression = Helper.GetLamdaExpression("LocalVariableTestClass", "AssignParameterToLocalVariableWithDeclaration");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke("hello");

            Tracker.LastValue.Should().Be("hello");
        }
    }
}
