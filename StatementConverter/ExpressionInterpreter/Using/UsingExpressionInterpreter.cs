using System;
using System.Linq.Expressions;
using Microsoft.CSharp.Expressions;

namespace StatementConverter.ExpressionInterpreter
{
    internal class UsingExpressionInterpreter : IExpressionInterpreter
    {
        private readonly ExpressionInterpreterHandler expressionInterpreterHandler;
        private readonly HotReloading.Core.Statements.UsingStatement usingStatement;
        private readonly System.Collections.Generic.List<ParameterExpression> scopedLocalVariable;

        public UsingExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, HotReloading.Core.Statements.UsingStatement usingStatement, System.Collections.Generic.List<ParameterExpression> scopedLocalVariable)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.usingStatement = usingStatement;
            this.scopedLocalVariable = scopedLocalVariable;
        }

        public Expression GetExpression()
        {
            ParameterExpression variable = null;
            if(usingStatement.Variable != null)
            {
                variable = expressionInterpreterHandler.GetExpression(usingStatement.Variable) as ParameterExpression;

                scopedLocalVariable.Add(variable);
            }
            var resource = expressionInterpreterHandler.GetExpression(usingStatement.Resource);
            var body = expressionInterpreterHandler.GetExpression(usingStatement.Body);
            if(variable != null)
            {
                scopedLocalVariable.Remove(variable);
                return CSharpExpression.Using(variable, resource, body);
            }
            return CSharpExpression.Using(resource, body);
        }
    }
}