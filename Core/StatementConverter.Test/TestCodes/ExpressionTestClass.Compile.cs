using System;
using System.Linq.Expressions;

namespace StatementConverter.Test
{
    public partial class ExpressionTestClass
    {
        private void TestFunction(Expression<Action> expression)
        {
            expression.Compile()();
        }
    }
}
