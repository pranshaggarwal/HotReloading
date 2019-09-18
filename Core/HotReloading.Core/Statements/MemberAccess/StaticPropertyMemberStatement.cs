namespace HotReloading.Core.Statements
{
    public class StaticPropertyMemberStatement : MemberAccessStatement
    {
        public HrType ParentType { get; set; }
        public string Name { get; set; }
    }
}