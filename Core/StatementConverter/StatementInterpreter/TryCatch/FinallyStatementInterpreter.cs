using System;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class FinallyStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private FinallyClauseSyntax finallyClauseSyntax;

        public FinallyStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, FinallyClauseSyntax finallyClauseSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.finallyClauseSyntax = finallyClauseSyntax;
        }

        public Statement GetStatement()
        {
            return statementInterpreterHandler.GetStatement(finallyClauseSyntax.Block);
        }
    }
}