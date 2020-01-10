using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class BreakStatementInterpreter : IStatementInterpreter
    {
        private BreakStatementSyntax breakStatementSyntax;

        public BreakStatementInterpreter(BreakStatementSyntax breakStatementSyntax)
        {
            this.breakStatementSyntax = breakStatementSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            return new BreakStatement();
        }
    }
}