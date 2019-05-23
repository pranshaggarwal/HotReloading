using System;
using System.Linq.Expressions;
using HotReloading.Core.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class DelegateIdentifierExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private DelegateIdentifierStatement delegateIdentifierStatement;

        public DelegateIdentifierExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, DelegateIdentifierStatement delegateIdentifierStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.delegateIdentifierStatement = delegateIdentifierStatement;
        }

        public Expression GetExpression()
        {
            return expressionInterpreterHandler.GetExpression(delegateIdentifierStatement.Target);
        }
    }
}