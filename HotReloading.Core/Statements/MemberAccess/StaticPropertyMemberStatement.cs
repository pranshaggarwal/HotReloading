namespace HotReloading.Core.Statements
{
    public class StaticPropertyMemberStatement : MemberAccessStatement
    {
        public ClassType ParentType { get; set; }
        public string Name { get; set; }
    }
}