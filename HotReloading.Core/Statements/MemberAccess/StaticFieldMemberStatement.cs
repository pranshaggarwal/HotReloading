namespace HotReloading.Core.Statements
{
    public class StaticFieldMemberStatement : MemberAccessStatement
    {
        public ClassType ParentType { get; set; }
        public string Name { get; set; }
    }
}