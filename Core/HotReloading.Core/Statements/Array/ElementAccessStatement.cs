using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class ElementAccessStatement : Statement
    {
        public Statement Array { get; set; }
        public List<Statement> Indexes { get; set; }
        public Type Type { get; set; }
    }
}
