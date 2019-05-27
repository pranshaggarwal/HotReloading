using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CSharpExpressions.Microsoft.CSharp;
using HotReloading.Core;
using HotReloading.Core.Statements;
using StatementConverter.Extensions;

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
            var methodKey = GetMethodKey();

            var lamdaExpression = RuntimeMemory.GetMethod(invocationStatement.Method.ParentType, methodKey);

            if (lamdaExpression != null)
            {
                return InvokeLamdaExpression(lamdaExpression);
            }

            return InvokeMethod();
        }

        private Expression InvokeMethod()
        {
            var instanceMember = invocationStatement.Method as InstanceMethodMemberStatement;

            BindingFlags bindingFlags = instanceMember == null ? BindingFlags.Static : BindingFlags.Instance;

            bindingFlags |= invocationStatement.Method.AccessModifier == HotReloading.Core.AccessModifier.Public ?
                BindingFlags.Public : BindingFlags.NonPublic;

            Type declareType = (Type)invocationStatement.Method.ParentType;
            var parameterTypes = invocationStatement.ParametersSignature.Select(x => (Type)x).ToArray();

            var methodInfo =
                declareType.GetMethod(invocationStatement.Method.Name,
                    bindingFlags, Type.DefaultBinder, parameterTypes, null);

            var arguments = invocationStatement.Arguments
                           .Select(x => expressionInterpreterHandler.GetExpression(x)).ToArray();

            Expression[] convertedArguments = methodInfo.ConvertArguments(arguments);

            if (instanceMember != null)
            {
                var caller = expressionInterpreterHandler.GetExpression(instanceMember.Parent);
                return Expression.Call(
                            caller,
                            methodInfo, convertedArguments);
            }

            return Expression.Call(methodInfo, convertedArguments);
        }

        private Expression InvokeLamdaExpression(CSharpLamdaExpression lamdaExpression)
        {
            var instanceMember = invocationStatement.Method as InstanceMethodMemberStatement;

            var expression = lamdaExpression.GetExpression();
            var arguments = new List<Expression>();
            if (instanceMember != null)
                arguments.Add(expressionInterpreterHandler.GetExpression(instanceMember.Parent));
            arguments.AddRange(invocationStatement.Arguments
                .Select(x => expressionInterpreterHandler.GetExpression(x)).ToArray());
            return Expression.Invoke(expression, arguments);
        }

        private string GetMethodKey()
        {
            return HotReloading.Core.Helper.GetMethodKey(invocationStatement.Method.Name,
                invocationStatement.ParametersSignature.Select(x => ((Type)x).FullName).ToArray());
        }
    }
}