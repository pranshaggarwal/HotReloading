using System;
using System.Linq.Expressions;
using HotReloading.Core.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class DelegateObjectCreationExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private DelegateObjectCreationStatement delegateObjectCreationStatement;

        public DelegateObjectCreationExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, DelegateObjectCreationStatement delegateObjectCreationStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.delegateObjectCreationStatement = delegateObjectCreationStatement;
        }

        public Expression GetExpression()
        {
            return expressionInterpreterHandler.GetExpression(delegateObjectCreationStatement.Method);
        }
    }
}