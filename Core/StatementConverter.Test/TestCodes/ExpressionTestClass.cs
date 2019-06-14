using System;
using System.Linq.Expressions;

namespace StatementConverter.Test
{
    public partial class ExpressionTestClass
    {
        public void AssignExpression()
        {
            Expression<Action> expression = () => Tracker.Call("hello");

            var del = expression.Compile();
            del();
        }

        public void PassExpressionInMethod()
        {
            Expression<Action> expression = () => Tracker.Call("hello");
            TestFunction(expression);
        }

        private void TestFunction(Expression<Action> expression)
        {
            expression.Compile()();
        }
    }
}
