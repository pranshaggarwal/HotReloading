using HotReloading.Syntax;
using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    public class LiteralStatementInterpreter : IStatementInterpreter
    {
        private readonly LiteralExpressionSyntax literalExpressionSyntax;
        private readonly Microsoft.CodeAnalysis.SemanticModel semanticModel;

        public LiteralStatementInterpreter(LiteralExpressionSyntax literalExpressionSyntax, Microsoft.CodeAnalysis.SemanticModel semanticModel)
        {
            this.literalExpressionSyntax = literalExpressionSyntax;
            this.semanticModel = semanticModel;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            var typeInfo = semanticModel.GetTypeInfo(literalExpressionSyntax);
            var constantStatement = new ConstantStatement(literalExpressionSyntax.Token.Value);

            if(literalExpressionSyntax.Token.Value != null)
            {
                constantStatement.Type = typeInfo.GetHrType();
            }
            return constantStatement;
        }
    }
}