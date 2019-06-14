using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class DelegateInvocationStatement : Statement
    {
        public DelegateIdentifierStatement Delegate { get; set; }
        public IEnumerable<Statement> Arguments { get; set; }
    }
}