using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class ObjectEqualStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Left { get; set; }
        public IStatementCSharpSyntax Right { get; set; }
    }
}