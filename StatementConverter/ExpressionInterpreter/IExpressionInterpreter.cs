using System.Linq.Expressions;

namespace StatementConverter.ExpressionInterpreter
{
    public interface IExpressionInterpreter
    {
        Expression GetExpression();
    }
}