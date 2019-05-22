using System;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class ThrowStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private ThrowStatementSyntax throwStatementSyntax;

        public ThrowStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, ThrowStatementSyntax throwStatementSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.throwStatementSyntax = throwStatementSyntax;
        }

        public Statement GetStatement()
        {
            return new ThrowStatement
            {
                Statement = statementInterpreterHandler.GetStatement(throwStatementSyntax.Expression)
            };
        }
    }
}