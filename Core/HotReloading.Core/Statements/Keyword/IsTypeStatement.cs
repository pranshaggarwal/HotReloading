using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class IsTypeStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Statement { get; set; }
        public HrType Type { get; set; }
    }
}
