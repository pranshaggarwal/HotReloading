using System;
using System.Linq.Expressions;
using System.Reflection;
using HotReloading.Core.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    public class StaticFieldExpressionInterpreter : IExpressionInterpreter
    {
        private readonly StaticFieldMemberStatement staticFieldMemberStatement;

        public StaticFieldExpressionInterpreter(StaticFieldMemberStatement staticFieldMemberStatement)
        {
            this.staticFieldMemberStatement = staticFieldMemberStatement;
        }

        public Expression GetExpression()
        {
            var bindingFlags = BindingFlags.Static;
            bindingFlags |= staticFieldMemberStatement.AccessModifier == HotReloading.Core.AccessModifier.Public ?
                BindingFlags.Public : BindingFlags.NonPublic;
            var fieldInfo = ((Type) staticFieldMemberStatement.ParentType).GetField(staticFieldMemberStatement.Name, bindingFlags);
            return Expression.Field(null, fieldInfo);
        }
    }
}