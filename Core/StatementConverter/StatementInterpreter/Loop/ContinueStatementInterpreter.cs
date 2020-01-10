using System;
using HotReloading.Syntax.Statements;

namespace StatementConverter.StatementInterpreter
{
    internal class ContinueStatementInterpreter : IStatementInterpreter
    {
        public IStatementCSharpSyntax GetStatement()
        {
            return new ContinueStatement();
        }
    }
}