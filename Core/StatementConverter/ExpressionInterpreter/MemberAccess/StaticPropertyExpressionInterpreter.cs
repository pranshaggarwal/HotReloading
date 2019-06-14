using System;
using System.Linq.Expressions;
using System.Reflection;
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
            var bindingFlags = BindingFlags.Static;
            bindingFlags |= statement.AccessModifier == HotReloading.Core.AccessModifier.Public ?
                BindingFlags.Public : BindingFlags.NonPublic;
            var propertyInfo = ((Type) statement.ParentType).GetProperty(statement.Name, bindingFlags);
            return Expression.Property(null, propertyInfo);
        }
    }
}