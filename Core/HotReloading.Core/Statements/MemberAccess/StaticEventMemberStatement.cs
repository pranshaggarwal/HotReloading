namespace HotReloading.Core.Statements
{
    public class StaticEventMemberStatement : MemberAccessStatement
    {
        public HrType ParentType { get; set; }
        public string Name { get; set; }
    }
}