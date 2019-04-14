using System.Linq.Expressions;
using HotReloading.Core.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class ConditionalExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private ConditionalStatement conditionalStatement;

        public ConditionalExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, ConditionalStatement conditionalStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.conditionalStatement = conditionalStatement;
        }

        public Expression GetExpression()
        {
            return Expression.Condition(expressionInterpreterHandler.GetExpression(conditionalStatement.Condition),
                expressionInterpreterHandler.GetExpression(conditionalStatement.IfTrue),
                expressionInterpreterHandler.GetExpression(conditionalStatement.IfFalse));
        }
    }
}