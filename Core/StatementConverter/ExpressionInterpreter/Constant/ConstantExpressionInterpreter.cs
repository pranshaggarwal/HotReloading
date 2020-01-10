using System.Linq.Expressions;
using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    public class ConstantExpressionInterpreter : IExpressionInterpreter
    {
        private readonly ConstantStatement statement;

        public ConstantExpressionInterpreter(ConstantStatement statement)
        {
            this.statement = statement;
        }

        public Expression GetExpression()
        {
            if(statement.Type != null)
            {
                return Expression.Convert(Expression.Constant(statement.Value), statement.Type);
            }
            return Expression.Constant(statement.Value);
        }
    }
}