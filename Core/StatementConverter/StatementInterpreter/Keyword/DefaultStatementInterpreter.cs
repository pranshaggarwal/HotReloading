using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    internal class DefaultStatementInterpreter : IStatementInterpreter
    {
        private readonly Microsoft.CodeAnalysis.SemanticModel semanticModel;
        private DefaultExpressionSyntax defaultExpressionSyntax;

        public DefaultStatementInterpreter(Microsoft.CodeAnalysis.SemanticModel semanticModel, DefaultExpressionSyntax defaultExpressionSyntax)
        {
            this.semanticModel = semanticModel;
            this.defaultExpressionSyntax = defaultExpressionSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            return new DefaultStatement
            {
                Type = defaultExpressionSyntax.Type.GetClassType(semanticModel)
            };
        }
    }
}