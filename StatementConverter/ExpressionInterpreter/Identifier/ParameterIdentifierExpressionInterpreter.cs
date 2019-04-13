using System.Linq;
using System.Linq.Expressions;
using HotReloading.Core.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class ParameterIdentifierExpressionInterpreter : IExpressionInterpreter
    {
        private readonly ParameterExpression[] parameterExpressions;
        private readonly ParameterIdentifierStatement parameterIdentifierStatement;

        public ParameterIdentifierExpressionInterpreter(ParameterIdentifierStatement parameterIdentifierStatement,
            ParameterExpression[] parameterExpressions)
        {
            this.parameterIdentifierStatement = parameterIdentifierStatement;
            this.parameterExpressions = parameterExpressions;
        }

        public Expression GetExpression()
        {
            return parameterExpressions.FirstOrDefault(x => x.Name == parameterIdentifierStatement.Name);
        }
    }
}