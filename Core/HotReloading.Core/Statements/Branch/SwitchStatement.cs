using System;
using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class SwitchStatement : Statement
    {
        public Statement SwitchValue { get; set; }
        public Statement Default { get; set; }
        public SwitchCase[] Cases { get; set; }
    }
}
