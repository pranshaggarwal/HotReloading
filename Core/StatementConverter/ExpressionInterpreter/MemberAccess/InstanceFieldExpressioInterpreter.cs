using System.Linq.Expressions;
using System.Reflection;
using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

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
            var parent = interpreterHandler.GetExpression(statement.Parent);
            var bindingFlags = BindingFlags.Instance;
            bindingFlags |= statement.AccessModifier == AccessModifier.Public ?
                BindingFlags.Public : BindingFlags.NonPublic;

            var field = parent.Type.GetField(statement.Name, bindingFlags);
            return Expression.Field(parent, field);
        }
    }
}