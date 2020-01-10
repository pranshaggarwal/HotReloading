using System;
using System.Linq.Expressions;
using HotReloading.Syntax.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class ObjectEqualExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private ObjectEqualStatement objectEqualStatement;

        public ObjectEqualExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, ObjectEqualStatement objectEqualStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.objectEqualStatement = objectEqualStatement;
        }

        public Expression GetExpression()
        {
            var left = expressionInterpreterHandler.GetExpression(objectEqualStatement.Left);
            var right = expressionInterpreterHandler.GetExpression(objectEqualStatement.Right);
            var equalMethod = typeof(object).GetMethod("Equals", new Type[] { typeof(object), typeof(object)});
            return Expression.Call(equalMethod, left, right);
        }
    }
}