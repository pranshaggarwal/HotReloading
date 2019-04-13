using System.Linq.Expressions;
using FluentAssertions;
using HotReloading.Core;
using HotReloading.Core.Statements;
using NUnit.Framework;
using StatementConverter.ExpressionInterpreter;

namespace StatementConverter.Test.UnitTests.ExpressionInterpreter
{
    [TestFixture]
    public class StaticInvocationExpressionInterpreterTest
    {
        [Test]
        public void GetExpression_WhenStaticInvocationStatement_ReturnMethodCallExpression()
        {
            //var staticInvocationStatement = new StaticInvocationStatement
            //{
            //    MethodName = "WriteLine",
            //    ParentType = new ClassType
            //    {
            //        TypeString = "System.Diagnostics.Debug"
            //    },
            //    Arguments = new System.Collections.Generic.List<Argument>
            //    {
            //        new ConstantArgument
            //        {
            //            Type = new ClassType
            //            {
            //                TypeString = "System.String"
            //            },
            //            Value = "Hello"
            //        }
            //    }
            //};

            //var method = new Method { Parameters = new System.Collections.Generic.List<Parameter>() };

            //method.IsStatic = true;

            //var interpreter = new StaticInvocationExpressionInterpreter(new ExpressionInterpreterHandler(method), staticInvocationStatement);

            //var expression = interpreter.GetExpression();

            //var callExpression = expression.Should().BeAssignableTo<MethodCallExpression>().Subject;

            //callExpression.Method.Name.Should().Be("WriteLine");

            //callExpression.Arguments[0].NodeType.Should().Be(ExpressionType.Constant);
        }
    }
}
