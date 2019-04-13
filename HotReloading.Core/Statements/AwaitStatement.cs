using System;

namespace HotReloading.Core.Statements
{
    public class AwaitStatement : Statement
    {
        public Statement Statement { get; set; }
    }
}
