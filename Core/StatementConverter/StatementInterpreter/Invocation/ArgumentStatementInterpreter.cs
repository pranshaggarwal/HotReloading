using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    public class ArgumentStatementInterpreter : IStatementInterpreter
    {
        private readonly ArgumentSyntax argumentSyntax;
        private readonly StatementInterpreterHandler statementInterpreterHandler;

        public ArgumentStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler,
            ArgumentSyntax argumentSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.argumentSyntax = argumentSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            return statementInterpreterHandler.GetStatement(argumentSyntax.Expression);
        }
    }
}