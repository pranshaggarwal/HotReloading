using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HotReloading.Core.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    public class InvocationExpressionInterpreter : IExpressionInterpreter
    {
        private readonly ExpressionInterpreterHandler expressionInterpreterHandler;
        private readonly InvocationStatement invocationStatement;

        public InvocationExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler,
            InvocationStatement invocationStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.invocationStatement = invocationStatement;
        }

        public Expression GetExpression()
        {
            var suppliedArgs = invocationStatement.Arguments;
            var arguments = invocationStatement.Arguments
                .Select(x => expressionInterpreterHandler.GetExpression(x)).ToArray();

            var parameterTypes = invocationStatement.ParametersSignature.Select(x => (Type)x).ToArray();

            var methodInfo =
                ((Type) invocationStatement.Method.ParentType).GetMethod(invocationStatement.Method.Name,
                    parameterTypes);

            Expression[] convertedArguments = new Expression[arguments.Length];

            for(int i=0; i < convertedArguments.Length; i++)
            {
                var argument = arguments[i];

                var parameter = methodInfo.GetParameters()[i];

                if(parameter.ParameterType != argument.Type)
                {
                    convertedArguments[i] = Expression.Convert(argument, parameter.ParameterType);
                }
                else
                {
                    convertedArguments[i] = argument;
                }
            }

            if (invocationStatement.Method is InstanceMethodMemberStatement instanceStatement)
                return Expression.Call(
                    expressionInterpreterHandler.GetExpression(instanceStatement.Parent),
                    methodInfo, convertedArguments);

            return Expression.Call(methodInfo, convertedArguments);
        }
    }
}