using System;
using HotReloading.Syntax.Statements;
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

        public IStatementCSharpSyntax GetStatement()
        {
            return statementInterpreterHandler.GetStatement(parenthesizedExpressionSyntax.Expression);
        }
    }
}