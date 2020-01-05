using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class ThisStatement : IStatementCSharpSyntax
    {
        public BaseHrType Type { get; set; }
    }
}