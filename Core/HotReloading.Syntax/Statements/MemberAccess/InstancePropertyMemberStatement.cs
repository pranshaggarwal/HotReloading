namespace HotReloading.Syntax.Statements
{
    public class InstancePropertyMemberStatement : MemberAccessStatement
    {
        public IStatementCSharpSyntax Parent { get; set; }
        public string Name { get; set; }
    }
}