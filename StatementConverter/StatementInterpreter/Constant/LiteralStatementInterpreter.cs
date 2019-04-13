using HotReloading.Core.Statements;
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

        public Statement GetStatement()
        {
            var typeInfo = semanticModel.GetTypeInfo(literalExpressionSyntax);
            var constantStatement = new ConstantStatement(literalExpressionSyntax.Token.Value);

            if(literalExpressionSyntax.Token.Value != null)
            {
                constantStatement.Type = typeInfo.GetClassType();
            }
            return constantStatement;
        }
    }
}