using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class TryStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax TryBlock { get; set; }
        public List<CatchStatement> Catches { get; set; }
        public IStatementCSharpSyntax Finally { get; set; }

        public TryStatement()
        {
            Catches = new List<CatchStatement>();
        }
    }
}