using System;
using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class IfStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Condition { get; set; }
        public IStatementCSharpSyntax IfTrue { get; set; }
        public IStatementCSharpSyntax IfFalse { get; set; }
    }
}
