using System.Linq.Expressions;
using HotReloading.Core.Statements;

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