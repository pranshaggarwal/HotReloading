using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class CatchStatement : IStatementCSharpSyntax
    {
        public BaseHrType Type { get; set; }
        public LocalVariableDeclaration Variable { get; set; }
        public IStatementCSharpSyntax Block { get; set; }
        public IStatementCSharpSyntax Filter { get; set; }
    }
}