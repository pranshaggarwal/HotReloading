using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    public class ThisStatementInterpreter : IStatementInterpreter
    {
        private readonly SemanticModel semanticModel;
        private readonly ThisExpressionSyntax thisExpressionSyntax;

        public ThisStatementInterpreter(ThisExpressionSyntax thisExpressionSyntax, SemanticModel semanticModel)
        {
            this.thisExpressionSyntax = thisExpressionSyntax;
            this.semanticModel = semanticModel;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            var type = semanticModel.GetTypeInfo(thisExpressionSyntax).GetHrType();
            return new ThisStatement
            {
                Type = type
            };
        }
    }
}