using HotReloading.Syntax.Statements;

namespace HotReloading.Syntax
{
    public class Parameter : IStatementCSharpSyntax
    {
        public string Name { get; set; }
        public BaseHrType Type { get; set; }
    }
}