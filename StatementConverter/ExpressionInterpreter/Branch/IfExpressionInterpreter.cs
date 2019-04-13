using System.Linq.Expressions;
using HotReloading.Core.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class IfExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private IfStatement ifStatement;

        public IfExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, IfStatement ifStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.ifStatement = ifStatement;
        }

        public Expression GetExpression()
        {
            var condition = expressionInterpreterHandler.GetExpression(ifStatement.Condition);
            var ifTrue = expressionInterpreterHandler.GetExpression(ifStatement.IfTrue);
            if(ifStatement.IfFalse == null)
            {
                return Expression.IfThen(condition, ifTrue);
            }
            {
                var ifFalse = expressionInterpreterHandler.GetExpression(ifStatement.IfFalse);

                return Expression.IfThenElse(condition, ifTrue, ifFalse);
            }
        }
    }
}