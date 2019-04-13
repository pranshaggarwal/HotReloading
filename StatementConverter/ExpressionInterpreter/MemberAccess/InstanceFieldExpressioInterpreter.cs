using System.Linq.Expressions;
using HotReloading.Core.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    public class InstanceFieldExpressioInterpreter : IExpressionInterpreter
    {
        private readonly ExpressionInterpreterHandler interpreterHandler;
        private readonly InstanceFieldMemberStatement statement;

        public InstanceFieldExpressioInterpreter(ExpressionInterpreterHandler interpreterHandler,
            InstanceFieldMemberStatement statement)
        {
            this.interpreterHandler = interpreterHandler;
            this.statement = statement;
        }

        public Expression GetExpression()
        {
            return Expression.Field(interpreterHandler.GetExpression(statement.Parent),
                statement.Name);
        }
    }
}