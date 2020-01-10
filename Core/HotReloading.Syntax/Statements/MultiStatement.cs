using System.Collections.Generic;

namespace HotReloading.Syntax.Statements
{
    public class MultiStatement : IStatementCSharpSyntax
    {
        public IEnumerable<IStatementCSharpSyntax> Statements;
    }
}