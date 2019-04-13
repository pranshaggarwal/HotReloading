using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class InitializerStatement : Statement
    {
        public List<Statement> Statements { get; set; }
    }
}