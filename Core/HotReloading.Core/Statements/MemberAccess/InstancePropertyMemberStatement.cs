using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class InstancePropertyMemberStatement : MemberAccessStatement
    {
        public IStatementCSharpSyntax Parent { get; set; }
        public string Name { get; set; }
    }
}