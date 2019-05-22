using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CSharpExpressions.Microsoft.CSharp;
using HotReloading.Core;
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
            if (invocationStatement.Method is DelegateMethodMemberStatement delegateMethodMemberStatement)
            {
                return InvokeDelegateExpression();
            }

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

            Expression[] convertedArguments = GetArgumentsExpression(methodInfo);

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

        private Expression InvokeDelegateExpression()
        {
            Type declareType = (Type)invocationStatement.Method.ParentType;

            var methodInfo =
                declareType.GetMethod("Invoke");
            Expression[] convertedArguments = GetArgumentsExpression(methodInfo);

            var caller = expressionInterpreterHandler.GetExpression(((DelegateMethodMemberStatement)invocationStatement.Method).Delegate);
            return Expression.Call(
                        caller,
                        methodInfo, convertedArguments);

        }

        private Expression[] GetArgumentsExpression(MethodInfo methodInfo)
        {
            var arguments = invocationStatement.Arguments
                           .Select(x => expressionInterpreterHandler.GetExpression(x)).ToArray();
            Expression[] convertedArguments = new Expression[arguments.Length];

            for (int i = 0; i < convertedArguments.Length; i++)
            {
                var argument = arguments[i];

                var parameter = methodInfo.GetParameters()[i];

                if (parameter.ParameterType != argument.Type)
                {
                    convertedArguments[i] = Expression.Convert(argument, parameter.ParameterType);
                }
                else
                {
                    convertedArguments[i] = argument;
                }
            }

            return convertedArguments;
        }

        private Type GetDelegateType(MethodInfo methodInfo)
        {
            var parameterTypes = methodInfo.GetParameters().Select(x => x.ParameterType).ToList();
            parameterTypes.Add(methodInfo.ReturnType);
            return Expression.GetDelegateType(parameterTypes.ToArray());
        }

        private string GetMethodKey()
        {
            string key = invocationStatement.Method.Name;

            foreach (var type in invocationStatement.ParametersSignature)
            {
                key += $"`{((Type)type).FullName}";
            }
            return key;
        }
    }
}