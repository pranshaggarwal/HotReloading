using System;
using System.Linq.Expressions;
using HotReloading.Syntax.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class ThrowExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private ThrowStatement throwStatement;

        public ThrowExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, ThrowStatement throwStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.throwStatement = throwStatement;
        }

        public Expression GetExpression()
        {
            return Expression.Throw(expressionInterpreterHandler.GetExpression(throwStatement.Statement));
        }
    }
}