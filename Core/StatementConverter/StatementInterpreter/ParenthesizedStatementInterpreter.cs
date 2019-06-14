using System;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class ParenthesizedStatementInterpreter : IStatementInterpreter
    {
        private readonly StatementInterpreterHandler statementInterpreterHandler;
        private ParenthesizedExpressionSyntax parenthesizedExpressionSyntax;

        public ParenthesizedStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, ParenthesizedExpressionSyntax parenthesizedExpressionSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.parenthesizedExpressionSyntax = parenthesizedExpressionSyntax;
        }

        public Statement GetStatement()
        {
            return statementInterpreterHandler.GetStatement(parenthesizedExpressionSyntax.Expression);
        }
    }
}