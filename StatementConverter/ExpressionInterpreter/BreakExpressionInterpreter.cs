using System;
using System.Linq.Expressions;

namespace StatementConverter.ExpressionInterpreter
{
    internal class BreakExpressionInterpreter : IExpressionInterpreter
    {
        private LabelTarget brk;

        public BreakExpressionInterpreter(LabelTarget brk)
        {
            this.brk = brk;
        }

        public Expression GetExpression()
        {
            if (brk == null)
                throw new Exception("Not able to find break target");
            return Expression.Break(brk);
        }
    }
}