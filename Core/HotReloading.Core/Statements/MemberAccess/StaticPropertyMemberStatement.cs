namespace HotReloading.Core.Statements
{
    public class StaticPropertyMemberStatement : MemberAccessStatement
    {
        public Type ParentType { get; set; }
        public string Name { get; set; }
    }
}