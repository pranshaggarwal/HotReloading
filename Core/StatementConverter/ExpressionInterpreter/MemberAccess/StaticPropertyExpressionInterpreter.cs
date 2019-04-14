using System;
using System.Linq.Expressions;
using HotReloading.Core.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    public class StaticPropertyExpressionInterpreter : IExpressionInterpreter
    {
        private readonly StaticPropertyMemberStatement statement;

        public StaticPropertyExpressionInterpreter(StaticPropertyMemberStatement statement)
        {
            this.statement = statement;
        }

        public Expression GetExpression()
        {
            var propertyInfo = ((Type) statement.ParentType).GetProperty(statement.Name);
            return Expression.Property(null, propertyInfo);
        }
    }
}