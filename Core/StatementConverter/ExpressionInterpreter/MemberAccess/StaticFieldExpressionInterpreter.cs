using System;
using System.Linq.Expressions;
using System.Reflection;
using HotReloading.Core;
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
            var fieldKey = GetFieldKey();

            var inMemoryField = RuntimeMemory.GetField(staticFieldMemberStatement.ParentType, fieldKey);

            if (inMemoryField != null)
            {
                return GetInMemoryFieldExpression(inMemoryField);
            }
            var bindingFlags = BindingFlags.Static;
            bindingFlags |= staticFieldMemberStatement.AccessModifier == HotReloading.Core.AccessModifier.Public ?
                BindingFlags.Public : BindingFlags.NonPublic;
            var fieldInfo = ((Type) staticFieldMemberStatement.ParentType).GetField(staticFieldMemberStatement.Name, bindingFlags);
            return Expression.Field(null, fieldInfo);
        }

        private Expression GetInMemoryFieldExpression(FieldContainer inMemoryField)
        {
            var value = typeof(FieldContainer).GetProperty("Value");
            return Expression.Property(Expression.Constant(inMemoryField), value);
        }

        private string GetFieldKey()
        {
            return HotReloading.Core.Helper.GetFieldKey(staticFieldMemberStatement.Name);
        }
    }
}