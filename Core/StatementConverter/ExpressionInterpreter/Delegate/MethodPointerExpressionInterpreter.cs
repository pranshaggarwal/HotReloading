using System;
using System.Linq.Expressions;
using HotReloading.Syntax.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class MethodPointerExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private MethodPointerStatement methodPointerStatement;

        public MethodPointerExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, MethodPointerStatement methodPointerStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.methodPointerStatement = methodPointerStatement;
        }

        public Expression GetExpression()
        {
            if(methodPointerStatement.Method is InstanceMethodMemberStatement instanceMethodMemberStatement)
            {
                var createDelegateMethod = typeof(Delegate).GetMethod("CreateDelegate", new Type[] { typeof(Type), typeof(object), typeof(string) });
                return Expression.Call(null, createDelegateMethod,
                    Expression.Constant((Type)methodPointerStatement.Type),
                    expressionInterpreterHandler.GetExpression(instanceMethodMemberStatement.Parent),
                    Expression.Constant(instanceMethodMemberStatement.Name)
                    );
            }
            else if (methodPointerStatement.Method is StaticMethodMemberStatement staticMethodMemberStatement)
            {
                var createDelegateMethod = typeof(Delegate).GetMethod("CreateDelegate", new Type[] { typeof(Type), typeof(Type), typeof(string) });
                return Expression.Call(null, createDelegateMethod,
                    Expression.Constant((Type)methodPointerStatement.Type),
                    Expression.Constant((Type)staticMethodMemberStatement.ParentType),
                    Expression.Constant(staticMethodMemberStatement.Name)
                    );
            }

            throw new NotImplementedException("Unsupported method pointer: " + methodPointerStatement.Method.GetType().FullName);
        }
    }
}