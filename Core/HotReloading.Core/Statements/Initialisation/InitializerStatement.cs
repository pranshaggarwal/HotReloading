using System.Collections.Generic;
using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class InitializerStatement : IStatementCSharpSyntax
    {
        public List<IStatementCSharpSyntax> Statements { get; set; }
    }
}