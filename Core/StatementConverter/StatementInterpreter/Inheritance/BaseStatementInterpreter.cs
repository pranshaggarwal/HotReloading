using System;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class BaseStatementInterpreter : IStatementInterpreter
    {
        private BaseExpressionSyntax baseExpressionSyntax;

        public BaseStatementInterpreter(BaseExpressionSyntax baseExpressionSyntax)
        {
            this.baseExpressionSyntax = baseExpressionSyntax;
        }

        public Statement GetStatement()
        {
            return new BaseStatement();
        }
    }
}