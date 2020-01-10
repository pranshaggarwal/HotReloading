using System;
using HotReloading.Syntax.Statements;
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

        public IStatementCSharpSyntax GetStatement()
        {
            return new ThrowStatement
            {
                Statement = statementInterpreterHandler.GetStatement(throwStatementSyntax.Expression)
            };
        }
    }
}