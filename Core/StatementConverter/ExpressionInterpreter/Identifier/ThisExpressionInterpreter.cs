using System.Linq.Expressions;

namespace StatementConverter.ExpressionInterpreter
{
    public class ThisExpressionInterpreter : IExpressionInterpreter
    {
        private readonly ParameterExpression thisExpression;

        public ThisExpressionInterpreter(ParameterExpression thisExpression)
        {
            this.thisExpression = thisExpression;
        }

        public Expression GetExpression()
        {
            return thisExpression;
        }
    }
}