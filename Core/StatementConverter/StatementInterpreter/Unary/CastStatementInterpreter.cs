using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    internal class CastStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private CastExpressionSyntax castExpressionSyntax;
        private readonly Microsoft.CodeAnalysis.SemanticModel semanticModel;

        public CastStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, CastExpressionSyntax castExpressionSyntax, Microsoft.CodeAnalysis.SemanticModel semanticModel)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.castExpressionSyntax = castExpressionSyntax;
            this.semanticModel = semanticModel;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            var statement = new CastStatement
            {
                Statement = statementInterpreterHandler.GetStatement(castExpressionSyntax.Expression)
            };

            statement.Type = castExpressionSyntax.Type.GetClassType(semanticModel);

            return statement;
        }
    }
}