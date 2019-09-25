using HotReloading.Core.Statements;

namespace HotReloading.Core
{
    public class Parameter : IStatementCSharpSyntax
    {
        public string Name { get; set; }
        public BaseHrType Type { get; set; }
    }
}