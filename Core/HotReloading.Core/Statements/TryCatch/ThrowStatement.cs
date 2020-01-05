using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class ThrowStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Statement { get; set; }
    }
}