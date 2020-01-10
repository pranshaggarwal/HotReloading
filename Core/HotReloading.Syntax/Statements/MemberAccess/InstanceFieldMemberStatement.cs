namespace HotReloading.Syntax.Statements
{
    public class InstanceFieldMemberStatement : MemberAccessStatement
    {
        public IStatementCSharpSyntax Parent { get; set; }
        public string Name { get; set; }
    }
}