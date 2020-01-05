using System.Collections.Generic;
using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class MultiStatement : IStatementCSharpSyntax
    {
        public IEnumerable<IStatementCSharpSyntax> Statements;
    }
}