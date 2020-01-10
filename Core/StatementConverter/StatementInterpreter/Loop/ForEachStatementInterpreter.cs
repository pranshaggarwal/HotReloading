using System.Linq.Expressions;
using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    internal class ForEachStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private ForEachStatementSyntax forEachStatementSyntax;
        private readonly Microsoft.CodeAnalysis.SemanticModel semanticModel;

        public ForEachStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, ForEachStatementSyntax forEachStatementSyntax, Microsoft.CodeAnalysis.SemanticModel semanticModel)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.forEachStatementSyntax = forEachStatementSyntax;
            this.semanticModel = semanticModel;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            var parameterType = semanticModel.GetTypeInfo(forEachStatementSyntax.Type).GetHrType();

            var parameter = new LocalVariableDeclaration
            {
                Type = parameterType,
                Name = forEachStatementSyntax.Identifier.ValueText
            };

            var collection = statementInterpreterHandler.GetStatement(forEachStatementSyntax.Expression);

            var body = statementInterpreterHandler.GetStatement(forEachStatementSyntax.Statement);
            return new ForEachStatement
            {
                Variable = parameter,
                Collection = collection,
                Body = body
            };
        }
    }
}