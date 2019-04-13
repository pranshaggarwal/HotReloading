using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class ArrowStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private ArrowExpressionClauseSyntax arrowExpression;

        public ArrowStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, ArrowExpressionClauseSyntax arrowExpression)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.arrowExpression = arrowExpression;
        }

        public Statement GetStatement()
        {
            return statementInterpreterHandler.GetStatement(arrowExpression.Expression);
        }
    }
}