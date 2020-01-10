using System.Linq.Expressions;
using HotReloading.Syntax.Statements;

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
            Expression condition = expressionInterpreterHandler.GetExpression(conditionalStatement.Condition);
            Expression ifTrue = expressionInterpreterHandler.GetExpression(conditionalStatement.IfTrue);
            Expression ifFalse = expressionInterpreterHandler.GetExpression(conditionalStatement.IfFalse);

            if(ifTrue.Type != ifFalse.Type)
            {
                if (ifTrue.Type.IsSubclassOf(ifFalse.Type))
                    ifFalse = Expression.Convert(ifFalse, ifTrue.Type);
                else
                    ifTrue = Expression.Convert(ifTrue, ifFalse.Type);
            }
            return Expression.Condition(condition,
ifTrue,
ifFalse);
        }
    }
}