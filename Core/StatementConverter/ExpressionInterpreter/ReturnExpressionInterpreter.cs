using System.Linq.Expressions;
using HotReloading.Syntax.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class ReturnExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private ReturnStatement returnStatement;

        public ReturnExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, ReturnStatement returnStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.returnStatement = returnStatement;
        }

        public Expression GetExpression()
        {
            return expressionInterpreterHandler.GetExpression(returnStatement.Statement);
        }
    }
}