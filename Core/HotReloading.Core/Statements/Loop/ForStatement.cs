using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class ForStatement : IStatementCSharpSyntax
    {
        public List<IStatementCSharpSyntax> Initializers { get; set; }
        public IStatementCSharpSyntax Condition { get; set; }
        public List<IStatementCSharpSyntax> Iterators { get; set; }
        public IStatementCSharpSyntax Statement { get; set; }
    }
}
