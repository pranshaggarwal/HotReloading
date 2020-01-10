using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using HotReloading.Syntax.Statements;
using Microsoft.CSharp.Expressions;

namespace StatementConverter.ExpressionInterpreter
{
    internal class LamdaExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private LamdaStatement lamdaStatement;
        private readonly System.Collections.Generic.List<ParameterExpression> parameterExpressions;

        public LamdaExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, LamdaStatement lamdaStatement, System.Collections.Generic.List<ParameterExpression> parameterExpressions)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.lamdaStatement = lamdaStatement;
            this.parameterExpressions = parameterExpressions;
        }

        public Expression GetExpression()
        {
            var parameters = lamdaStatement.Parameters.Select(x =>
                                Expression.Parameter(x.Type, x.Name)).ToArray();
            parameterExpressions.AddRange(parameters);
            var body = expressionInterpreterHandler.GetExpression(lamdaStatement.Body);
            parameterExpressions.RemoveAll(x => parameters.Any(y => y == x));

            var type = (Type)lamdaStatement.Type;
            var delegateType = type;
            if (!type.IsSubclassOf(typeof(MulticastDelegate)))
            {
                delegateType = type.GenericTypeArguments.FirstOrDefault();
                MethodInfo lambdaMethod;
                if (lamdaStatement.IsAsync)
                    lambdaMethod = typeof(CSharpExpression).GetMethod("AsyncLambda", new Type[] { typeof(Type), typeof(Expression), typeof(ParameterExpression[]) });
                else
                    lambdaMethod = typeof(Expression).GetMethod("Lambda", new Type[] { typeof(Type), typeof(Expression), typeof(ParameterExpression[]) });

                return Expression.Call(null, lambdaMethod, Expression.Constant(delegateType), Expression.Constant(body), Expression.Constant(parameters));
            }

            Expression lamda;
            if (lamdaStatement.IsAsync)
                lamda = CSharpExpression.AsyncLambda(delegateType, body, parameters);
            else
                lamda = Expression.Lambda(delegateType, body, parameters);


            return lamda;
        }
    }
}