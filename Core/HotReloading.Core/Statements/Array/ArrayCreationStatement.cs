using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class ArrayCreationStatement : IStatementCSharpSyntax
    {
        public BaseHrType Type { get; set; }
        public List<IStatementCSharpSyntax> Bounds { get; set; }
        public List<IStatementCSharpSyntax> Initializers { get; set; }
    }
}
