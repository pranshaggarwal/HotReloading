using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public abstract class MemberAccessStatement : IStatementCSharpSyntax
    {
        public AccessModifier AccessModifier { get; set; }
    }
}