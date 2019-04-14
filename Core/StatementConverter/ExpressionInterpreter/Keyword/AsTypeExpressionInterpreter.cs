using System.Linq.Expressions;
using HotReloading.Core.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class AsTypeExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private AsStatement asTypeStatement;

        public AsTypeExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, AsStatement asTypeStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.asTypeStatement = asTypeStatement;
        }

        public Expression GetExpression()
        {
            var expression = expressionInterpreterHandler.GetExpression(asTypeStatement.Statement);
            return Expression.TypeAs(expression, asTypeStatement.Type);
        }
    }
}