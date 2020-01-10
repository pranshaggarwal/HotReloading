using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class ElseClauseStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private ElseClauseSyntax elseClauseSyntax;

        public ElseClauseStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, ElseClauseSyntax elseClauseSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.elseClauseSyntax = elseClauseSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            return statementInterpreterHandler.GetStatement(elseClauseSyntax.Statement);
        }
    }
}