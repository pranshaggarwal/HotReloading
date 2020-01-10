using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using HotReloading.Syntax;
using HotReloading.Syntax.Statements;
using StatementConverter.Extensions;

namespace StatementConverter.ExpressionInterpreter
{
    public class InstancePropertyExpressionInterpreter : IExpressionInterpreter
    {
        private readonly ExpressionInterpreterHandler interpreterHandler;
        private readonly InstancePropertyMemberStatement statement;

        public InstancePropertyExpressionInterpreter(ExpressionInterpreterHandler interpreterHandler,
            InstancePropertyMemberStatement statement)
        {
            this.interpreterHandler = interpreterHandler;
            this.statement = statement;
        }

        public Expression GetExpression()
        {
            var parentExpression = interpreterHandler.GetExpression(statement.Parent);
            var bindingFlags = BindingFlags.Instance;
            bindingFlags |= statement.AccessModifier == AccessModifier.Public ?
                BindingFlags.Public : BindingFlags.NonPublic;
            var property = parentExpression.Type.GetMostSuitableProperty(statement.Name, bindingFlags);

            return Expression.Property(parentExpression,
                property);
        }
    }
}