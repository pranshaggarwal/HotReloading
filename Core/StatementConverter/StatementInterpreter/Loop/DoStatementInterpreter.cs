using System;
using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class DoStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private DoStatementSyntax doStatementSyntax;

        public DoStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, DoStatementSyntax doStatementSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.doStatementSyntax = doStatementSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            var condition = statementInterpreterHandler.GetStatement(doStatementSyntax.Condition);
            var statement = statementInterpreterHandler.GetStatement(doStatementSyntax.Statement);

            return new DoWhileStatement
            {
                Conditional = condition,
                Statement = statement
            };
        }
    }
}