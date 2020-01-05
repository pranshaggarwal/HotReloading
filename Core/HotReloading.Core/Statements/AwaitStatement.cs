using System;
using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class AwaitStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Statement { get; set; }
    }
}
