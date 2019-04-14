using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class MultiStatement : Statement
    {
        public IEnumerable<Statement> Statements;
    }
}