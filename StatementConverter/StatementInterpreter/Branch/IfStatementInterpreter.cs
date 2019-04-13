using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class IfStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private IfStatementSyntax ifStatementSyntax;

        public IfStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, IfStatementSyntax ifStatementSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.ifStatementSyntax = ifStatementSyntax;
        }

        public Statement GetStatement()
        {
            return new IfStatement
            {
                Condition = statementInterpreterHandler.GetStatement(ifStatementSyntax.Condition),
                IfTrue = statementInterpreterHandler.GetStatement(ifStatementSyntax.Statement),
                IfFalse = ifStatementSyntax.Else != null ? statementInterpreterHandler.GetStatement(ifStatementSyntax.Else) : null,
            };
        }
    }
}