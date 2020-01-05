using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class DelegateIdentifierStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Target { get; set; }
        public BaseHrType Type { get; set; }
    }
}