using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class ForStatement : Statement
    {
        public List<Statement> Initializers { get; set; }
        public Statement Condition { get; set; }
        public List<Statement> Iterators { get; set; }
        public Statement Statement { get; set; }
    }
}
