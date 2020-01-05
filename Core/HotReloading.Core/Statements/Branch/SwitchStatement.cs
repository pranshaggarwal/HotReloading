using System;
using System.Collections.Generic;
using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class SwitchStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax SwitchValue { get; set; }
        public IStatementCSharpSyntax Default { get; set; }
        public SwitchCase[] Cases { get; set; }
    }
}
