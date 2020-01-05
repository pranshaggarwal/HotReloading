using System;
using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class DefaultStatement : IStatementCSharpSyntax
    {
        public HrType Type { get; set; }
    }
}
