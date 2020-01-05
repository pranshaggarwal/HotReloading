using System.Collections.Generic;
using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class ElementAccessStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Array { get; set; }
        public List<IStatementCSharpSyntax> Indexes { get; set; }
        public BaseHrType Type { get; set; }
    }
}
