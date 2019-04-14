using System;
using System.Linq.Expressions;
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
            var fieldInfo = ((Type) staticFieldMemberStatement.ParentType).GetField(staticFieldMemberStatement.Name);
            return Expression.Field(null, fieldInfo);
        }
    }
}