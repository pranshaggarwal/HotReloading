using System;
using System.Linq.Expressions;
using System.Reflection;
using HotReloading.Syntax.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class StaticEventExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private StaticEventMemberStatement staticEventMemberStatement;

        public StaticEventExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, StaticEventMemberStatement instanceEventMemberStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.staticEventMemberStatement = instanceEventMemberStatement;
        }

        public Expression GetExpression()
        {
            var bindingFlags = BindingFlags.Static;
            bindingFlags |= BindingFlags.NonPublic;
            var fieldInfo = ((Type)staticEventMemberStatement.ParentType).GetField(staticEventMemberStatement.Name,
                bindingFlags);

            return Expression.Field(null, fieldInfo);
        }
    }
}