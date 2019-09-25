using System;
using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class SwitchStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax SwitchValue { get; set; }
        public IStatementCSharpSyntax Default { get; set; }
        public SwitchCase[] Cases { get; set; }
    }
}
