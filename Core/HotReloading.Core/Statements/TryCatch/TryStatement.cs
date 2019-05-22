using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class TryStatement : Statement
    {
        public Statement TryBlock { get; set; }
        public List<CatchStatement> Catches { get; set; }
        public Statement Finally { get; set; }

        public TryStatement()
        {
            Catches = new List<CatchStatement>();
        }
    }
}