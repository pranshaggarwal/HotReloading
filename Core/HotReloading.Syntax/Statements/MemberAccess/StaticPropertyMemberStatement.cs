namespace HotReloading.Syntax.Statements
{
    public class StaticPropertyMemberStatement : MemberAccessStatement
    {
        public HrType ParentType { get; set; }
        public string Name { get; set; }
    }
}