using System;
using HotReloading.Core.Statements;
using HotReloading.Syntax.Statements;
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

        public IStatementCSharpSyntax GetStatement()
        {
            return new BaseStatement();
        }
    }
}