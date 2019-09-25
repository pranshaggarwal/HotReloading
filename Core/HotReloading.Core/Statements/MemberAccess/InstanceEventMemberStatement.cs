namespace HotReloading.Core.Statements
{
    public class InstanceEventMemberStatement : MemberAccessStatement
    {
        public HrType ParentType { get; set; }
        public string Name { get; set; }
        public IStatementCSharpSyntax Parent { get; set; }
    }
}