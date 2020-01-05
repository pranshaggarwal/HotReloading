using System;
using HotReloading.Core.Statements;
using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class WhileStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private WhileStatementSyntax whileStatementSyntax;

        public WhileStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, WhileStatementSyntax whileStatementSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.whileStatementSyntax = whileStatementSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            var condition = statementInterpreterHandler.GetStatement(whileStatementSyntax.Condition);
            var statement = statementInterpreterHandler.GetStatement(whileStatementSyntax.Statement);

            return new WhileStatement
            {
                Conditional = condition,
                Statement = statement
            };
        }
    }
}