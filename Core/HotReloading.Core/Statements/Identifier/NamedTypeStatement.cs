using HotReloading.Syntax;

namespace HotReloading.Core.Statements
{
    public class NamedTypeStatement : IdentifierStatement
    {
        public HrType Type { get; set; }
    }
}