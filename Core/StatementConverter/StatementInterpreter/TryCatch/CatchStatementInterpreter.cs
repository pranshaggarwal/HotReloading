using System;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    internal class CatchStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private CatchClauseSyntax catchClauseSyntax;
        private readonly Microsoft.CodeAnalysis.SemanticModel semanticModel;

        public CatchStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, 
            CatchClauseSyntax catchClauseSyntax, 
            Microsoft.CodeAnalysis.SemanticModel semanticModel)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.catchClauseSyntax = catchClauseSyntax;
            this.semanticModel = semanticModel;
        }

        public Statement GetStatement()
        {
            var catchStatement = new CatchStatement();

            catchStatement.Block = statementInterpreterHandler.GetStatement(catchClauseSyntax.Block);
            catchStatement.Filter = catchClauseSyntax.Filter == null ?
                                    null : statementInterpreterHandler.GetStatement(catchClauseSyntax.Filter.FilterExpression);

            if (catchClauseSyntax.Declaration != null)
            {
                catchStatement.Type = semanticModel.GetTypeInfo(catchClauseSyntax.Declaration.Type).GetClassType();

                if(!catchClauseSyntax.Declaration.Identifier.IsKind( Microsoft.CodeAnalysis.CSharp.SyntaxKind.None))
                {
                    catchStatement.Variable = new LocalVariableDeclaration
                    {
                        Type = catchStatement.Type,
                        Name = catchClauseSyntax.Declaration.Identifier.ValueText
                    };
                }
            }
            return catchStatement;
        }
    }
}