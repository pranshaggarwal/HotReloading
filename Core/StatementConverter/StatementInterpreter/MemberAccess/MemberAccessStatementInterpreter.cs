using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    public class MemberAccessStatementInterpreter : IStatementInterpreter
    {
        private readonly MemberAccessExpressionSyntax memberAccessExpressionSyntax;
        private readonly StatementInterpreterHandler statementInterpreterHandler;

        public MemberAccessStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler,
            MemberAccessExpressionSyntax memberAccessExpressionSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.memberAccessExpressionSyntax = memberAccessExpressionSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            var parent = statementInterpreterHandler.GetStatement(memberAccessExpressionSyntax.Expression);

            return statementInterpreterHandler.GetStatementInterpreter(memberAccessExpressionSyntax.Name, parent);
        }
    }
}