using System.Collections.Generic;

namespace HotReloading.Syntax.Statements
{
    public class DelegateInvocationStatement : IStatementCSharpSyntax
    {
        public DelegateIdentifierStatement Delegate { get; set; }
        public IEnumerable<IStatementCSharpSyntax> Arguments { get; set; }
    }
}