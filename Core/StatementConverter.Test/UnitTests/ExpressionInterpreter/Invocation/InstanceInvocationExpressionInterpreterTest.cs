using System;
using System.Linq.Expressions;
using FluentAssertions;
using HotReloading.Core;
using HotReloading.Core.Statements;
using NUnit.Framework;
using StatementConverter.ExpressionInterpreter;

namespace StatementConverter.Test.UnitTests.ExpressionInterpreter
{
    [TestFixture]
    public class InstanceInvocationExpressionInterpreterTest
    {
        [Test]
        public void GetExpression_WhenInstanceInvocationStatement_ReturnMethodCallExpression()
        {
            //var instanceInvocationStatement = new InstanceInvocationStatement
            //{
            //    MethodName = "ToUpper",
            //    ParentType = new ClassType
            //    {
            //        TypeString = "System.String"
            //    },
            //    Instance = "test"
            //};

            //var interpreter = new InstanceInvocationExpressionInterpreter(new ExpressionInterpreterHandler(new Method { Parameters = new System.Collections.Generic.List<Parameter>() }), instanceInvocationStatement);

            //var expression = interpreter.GetExpression();

            //var callExpression = expression.Should().BeAssignableTo<MethodCallExpression>().Subject;
            //callExpression.Method.Name.Should().Be("ToUpper");
        }
    }
}
