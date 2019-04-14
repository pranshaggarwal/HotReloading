using System.Linq;
using System.Linq.Expressions;
using HotReloading.Core.Statements;
using StatementConverter.Extensions;

namespace StatementConverter.ExpressionInterpreter
{
    public class InstancePropertyExpressionInterpreter : IExpressionInterpreter
    {
        private readonly ExpressionInterpreterHandler interpreterHandler;
        private readonly InstancePropertyMemberStatement statement;

        public InstancePropertyExpressionInterpreter(ExpressionInterpreterHandler interpreterHandler,
            InstancePropertyMemberStatement statement)
        {
            this.interpreterHandler = interpreterHandler;
            this.statement = statement;
        }

        public Expression GetExpression()
        {
            var parentExpression = interpreterHandler.GetExpression(statement.Parent);

            var property = parentExpression.Type.GetMostSuitableProperty(statement.Name);

            return Expression.Property(parentExpression,
                property);
        }
    }
}