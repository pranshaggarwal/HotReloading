using System;
using System.Linq;
using System.Linq.Expressions;
using HotReloading.Core.Statements;
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

            Expression lamda;
            if (lamdaStatement.IsAsync)
                lamda = CSharpExpression.AsyncLambda(lamdaStatement.Type, body, parameters);
            else
                lamda = Expression.Lambda(lamdaStatement.Type, body, parameters);

            return lamda;
        }
    }
}