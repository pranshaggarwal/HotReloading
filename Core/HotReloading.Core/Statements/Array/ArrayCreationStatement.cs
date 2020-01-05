using System.Collections.Generic;
using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class ArrayCreationStatement : IStatementCSharpSyntax
    {
        public BaseHrType Type { get; set; }
        public List<IStatementCSharpSyntax> Bounds { get; set; }
        public List<IStatementCSharpSyntax> Initializers { get; set; }
    }
}
