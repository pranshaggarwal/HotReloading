using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class ReturnStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Statement { get; set; }
    }
}