using System;
using System.Linq.Expressions;
using HotReloading.Core.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class DelegateObjectCreationExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private DelegateObjectCreationStatement delegateObjectCreationStatement;

        public DelegateObjectCreationExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, DelegateObjectCreationStatement delegateObjectCreationStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.delegateObjectCreationStatement = delegateObjectCreationStatement;
        }

        public Expression GetExpression()
        {
            if(delegateObjectCreationStatement.Method is InstanceMethodMemberStatement instanceMethodMemberStatement)
            {
                var createInstanceDelegateMethod = typeof(Delegate).GetMethod("CreateDelegate", new Type[] { typeof(Type), typeof(object), typeof(string) });
                return Expression.Call(null, createInstanceDelegateMethod,
                    Expression.Constant((Type)delegateObjectCreationStatement.Type),
                    expressionInterpreterHandler.GetExpression(instanceMethodMemberStatement.Parent),
                    Expression.Constant(instanceMethodMemberStatement.Name)
                    );
            }
            return null;
            //var createDelegateMethod = typeof(Delegate).GetMethod("CreateDelegate", new Type[] { typeof(Type), typeof(Type), typeof(string) });
            //return Expression.Call(null, createDelegateMethod,
                    //Expression.Constant((Type)delegateObjectCreationStatement.Type),
                    //Expression.Constant((Type)delegateObjectCreationStatement.Method),
                    //Expression.Constant(instanceMethodMemberStatement.Name)
                    //);
        }
    }
}