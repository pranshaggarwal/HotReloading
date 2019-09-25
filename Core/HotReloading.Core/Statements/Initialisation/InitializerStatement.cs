using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class InitializerStatement : IStatementCSharpSyntax
    {
        public List<IStatementCSharpSyntax> Statements { get; set; }
    }
}