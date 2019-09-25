using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class MultiStatement : IStatementCSharpSyntax
    {
        public IEnumerable<IStatementCSharpSyntax> Statements;
    }
}