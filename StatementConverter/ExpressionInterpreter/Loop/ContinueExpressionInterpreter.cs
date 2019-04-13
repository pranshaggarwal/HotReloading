using System;
using System.Linq.Expressions;

namespace StatementConverter.ExpressionInterpreter
{
    public class ContinueExpressionInterpreter : IExpressionInterpreter
    {
        private LabelTarget cont;

        public ContinueExpressionInterpreter(LabelTarget cont)
        {
            this.cont = cont;
        }

        public Expression GetExpression()
        {
            if (cont == null)
                throw new Exception("Not able to find break target");
            return Expression.Break(cont);
        }
    }
}