using HotReloading.Core.Statements;
using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class ConditionalStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private ConditionalExpressionSyntax conditionalExpressionSyntax;

        public ConditionalStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, ConditionalExpressionSyntax conditionalExpressionSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.conditionalExpressionSyntax = conditionalExpressionSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            return new ConditionalStatement
            {
                Condition = statementInterpreterHandler.GetStatement(conditionalExpressionSyntax.Condition),
                IfTrue = statementInterpreterHandler.GetStatement(conditionalExpressionSyntax.WhenTrue),
                IfFalse = statementInterpreterHandler.GetStatement(conditionalExpressionSyntax.WhenFalse),
            };
        }
    }
}