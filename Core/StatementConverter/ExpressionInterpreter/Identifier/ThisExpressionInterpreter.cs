using System.Linq.Expressions;
using HotReloading.Core.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    public class ThisExpressionInterpreter : IExpressionInterpreter
    {
        private readonly ParameterExpression thisExpression;
        private ThisStatement thisStatement;

        public ThisExpressionInterpreter(ThisStatement thisStatement, ParameterExpression thisExpression)
        {
            this.thisStatement = thisStatement;
            this.thisExpression = thisExpression;
        }

        public Expression GetExpression()
        {
            return thisExpression;
        }
    }
}