using System.Collections.Generic;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    public class LocalVariableStatementIntrepreter : IStatementInterpreter
    {
        private readonly StatementInterpreterHandler statementInterpreterHandler;
        private readonly LocalDeclarationStatementSyntax localDeclarationStatementSyntax;

        public LocalVariableStatementIntrepreter(StatementInterpreterHandler statementInterpreterHandler,
            LocalDeclarationStatementSyntax localDeclarationStatementSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.localDeclarationStatementSyntax = localDeclarationStatementSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            return statementInterpreterHandler.GetStatement(localDeclarationStatementSyntax.Declaration);
        }
    }
}