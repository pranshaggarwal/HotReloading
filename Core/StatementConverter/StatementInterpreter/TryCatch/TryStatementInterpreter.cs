using System;
using System.Linq;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class TryStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private TryStatementSyntax tryStatementSyntax;

        public TryStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, TryStatementSyntax tryStatementSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.tryStatementSyntax = tryStatementSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            var tryStatement = new TryStatement();
            tryStatement.TryBlock = statementInterpreterHandler.GetStatement(tryStatementSyntax.Block);

            tryStatement.Catches.AddRange(tryStatementSyntax.Catches.Select(x =>
                                            statementInterpreterHandler.GetStatement(x)).Cast<CatchStatement>());

            tryStatement.Finally = tryStatementSyntax.Finally == null ?
                null : statementInterpreterHandler.GetStatement(tryStatementSyntax.Finally);

            return tryStatement;
        }
    }
}