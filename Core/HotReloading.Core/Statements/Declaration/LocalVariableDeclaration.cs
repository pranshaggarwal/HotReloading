using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class LocalVariableDeclaration : IStatementCSharpSyntax
    {
        public BaseHrType Type { get; set; }
        public string Name { get; set; }
    }
}