namespace HotReloading.Syntax.Statements
{
    public class StaticFieldMemberStatement : MemberAccessStatement
    {
        public HrType ParentType { get; set; }
        public string Name { get; set; }
    }
}