namespace HotReloading.Core.Statements
{
    public class StaticEventMemberStatement : MemberAccessStatement
    {
        public ClassType ParentType { get; set; }
        public string Name { get; set; }
    }
}