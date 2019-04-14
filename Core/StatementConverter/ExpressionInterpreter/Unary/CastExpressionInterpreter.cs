using System.Linq.Expressions;
using HotReloading.Core.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class CastExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private CastStatement castStatement;

        public CastExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, CastStatement castStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.castStatement = castStatement;
        }

        public Expression GetExpression()
        {
            return Expression.Convert(expressionInterpreterHandler.GetExpression(castStatement.Statement),
                castStatement.Type);
        }
    }
}