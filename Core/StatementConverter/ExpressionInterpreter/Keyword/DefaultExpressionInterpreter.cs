using System.Linq.Expressions;
using HotReloading.Syntax.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class DefaultExpressionInterpreter : IExpressionInterpreter
    {
        private DefaultStatement defaultStatement;

        public DefaultExpressionInterpreter(DefaultStatement defaultStatement)
        {
            this.defaultStatement = defaultStatement;
        }

        public Expression GetExpression()
        {
            return Expression.Default(defaultStatement.Type);
        }
    }
}