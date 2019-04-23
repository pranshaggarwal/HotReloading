using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
            var suppliedArgs = invocationStatement.Arguments;
            var arguments = invocationStatement.Arguments
                .Select(x => expressionInterpreterHandler.GetExpression(x)).ToArray();

            var parameterTypes = invocationStatement.ParametersSignature.Select(x => (Type)x).ToArray();

            var methodKey = GetMethodKey();

            var lamdaExpression = RuntimeMemory.GetMethod(invocationStatement.Method.ParentType, methodKey);

            var instanceMember = invocationStatement.Method as InstanceMethodMemberStatement;

            if (lamdaExpression != null)
            {
                var expression = lamdaExpression.GetExpression();
                var arguments1 = new List<Expression>();
                if (instanceMember != null)
                    arguments1.Add(expressionInterpreterHandler.GetExpression(instanceMember.Parent));
                arguments1.AddRange(arguments);
                return Expression.Invoke(expression, arguments1);
            }

            BindingFlags bindingFlags = instanceMember == null ? BindingFlags.Static : BindingFlags.Instance;

            bindingFlags |= invocationStatement.Method.AccessModifier == HotReloading.Core.AccessModifier.Public ?
                BindingFlags.Public : BindingFlags.NonPublic;

            Type declareType = (Type)invocationStatement.Method.ParentType;
            var methodInfo =
                declareType.GetMethod(invocationStatement.Method.Name,
                    bindingFlags, Type.DefaultBinder, parameterTypes, null);
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

            if (instanceMember != null)
            {
                var caller = expressionInterpreterHandler.GetExpression(instanceMember.Parent);
                return Expression.Call(
                            caller,
                            methodInfo, convertedArguments);

            }

            return Expression.Call(methodInfo, convertedArguments);
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