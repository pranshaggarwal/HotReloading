using System.Linq.Expressions;
using HotReloading.Syntax.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class IsTypeExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private IsTypeStatement isTypeStatement;

        public IsTypeExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, IsTypeStatement isTypeStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.isTypeStatement = isTypeStatement;
        }

        public Expression GetExpression()
        {
            var expression = expressionInterpreterHandler.GetExpression(isTypeStatement.Statement);
            return Expression.TypeIs(expression, isTypeStatement.Type);
        }
    }
}