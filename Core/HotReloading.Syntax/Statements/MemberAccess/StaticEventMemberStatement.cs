namespace HotReloading.Syntax.Statements
{
    public class StaticEventMemberStatement : MemberAccessStatement
    {
        public HrType ParentType { get; set; }
        public string Name { get; set; }
    }
}