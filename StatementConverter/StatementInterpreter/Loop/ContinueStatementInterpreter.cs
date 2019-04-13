using System;
using HotReloading.Core.Statements;

namespace StatementConverter.StatementInterpreter
{
    internal class ContinueStatementInterpreter : IStatementInterpreter
    {
        public Statement GetStatement()
        {
            return new ContinueStatement();
        }
    }
}