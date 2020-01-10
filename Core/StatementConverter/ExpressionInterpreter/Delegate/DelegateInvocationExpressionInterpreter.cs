using System;
using System.Linq;
using System.Linq.Expressions;
using HotReloading.Syntax.Statements;
using StatementConverter.Extensions;

namespace StatementConverter.ExpressionInterpreter
{
    internal class DelegateInvocationExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private DelegateInvocationStatement delegateInvocationStatement;

        public DelegateInvocationExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, DelegateInvocationStatement delegateInvocationStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.delegateInvocationStatement = delegateInvocationStatement;
        }

        public Expression GetExpression()
        {
            Type declareType = (Type)delegateInvocationStatement.Delegate.Type;

            var methodInfo =
                declareType.GetMethod("Invoke");
            var arguments = delegateInvocationStatement.Arguments.Select(x =>
                expressionInterpreterHandler.GetExpression(x)).ToArray();
            Expression[] convertedArguments = methodInfo.ConvertArguments(arguments);

            var caller = expressionInterpreterHandler.GetExpression(delegateInvocationStatement.Delegate.Target);
            return Expression.Call(
                        caller,
                        methodInfo, convertedArguments);
        }
    }
}