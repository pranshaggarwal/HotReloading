using System;
using System.Linq.Expressions;
using System.Reflection;
using HotReloading.Syntax.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class InstanceEventExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private InstanceEventMemberStatement instanceEventMemberStatement;

        public InstanceEventExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, InstanceEventMemberStatement instanceEventMemberStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.instanceEventMemberStatement = instanceEventMemberStatement;
        }

        public Expression GetExpression()
        {
            var bindingFlags = BindingFlags.Instance;
            bindingFlags |= BindingFlags.NonPublic;
            var fieldInfo = ((Type)instanceEventMemberStatement.ParentType).GetField(instanceEventMemberStatement.Name, 
                bindingFlags);

            var parent = expressionInterpreterHandler.GetExpression(instanceEventMemberStatement.Parent);

            return Expression.Field(parent, fieldInfo);
        }
    }
}