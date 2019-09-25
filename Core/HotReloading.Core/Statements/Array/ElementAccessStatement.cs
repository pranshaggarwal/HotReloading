using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class ElementAccessStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Array { get; set; }
        public List<IStatementCSharpSyntax> Indexes { get; set; }
        public BaseHrType Type { get; set; }
    }
}
