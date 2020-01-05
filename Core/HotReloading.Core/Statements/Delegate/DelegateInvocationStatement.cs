using System.Collections.Generic;
using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class DelegateInvocationStatement : IStatementCSharpSyntax
    {
        public DelegateIdentifierStatement Delegate { get; set; }
        public IEnumerable<IStatementCSharpSyntax> Arguments { get; set; }
    }
}