using System.Linq.Expressions;
using HotReloading.Syntax.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    public class LocalVariableDeclarationExpressionInterpreter : IExpressionInterpreter
    {
        private readonly LocalVariableDeclaration localVariableDeclaration;

        public LocalVariableDeclarationExpressionInterpreter(LocalVariableDeclaration localVariableDeclaration)
        {
            this.localVariableDeclaration = localVariableDeclaration;
        }

        public Expression GetExpression()
        {
            return Expression.Parameter(localVariableDeclaration.Type, localVariableDeclaration.Name);
        }
    }
}