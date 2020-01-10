using System.Collections.Generic;

namespace HotReloading.Syntax.Statements
{
    public class InitializerStatement : IStatementCSharpSyntax
    {
        public List<IStatementCSharpSyntax> Statements { get; set; }
    }
}