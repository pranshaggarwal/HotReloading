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
            TestFunction(() => Tracker.Call("hello"));
        }

        private void TestFunction(Expression<Action> expression)
        {
            expression.Compile()();
        }
    }
}
