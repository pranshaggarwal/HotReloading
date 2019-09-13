namespace HotReloading.Core.Statements
{
    public class StaticEventMemberStatement : MemberAccessStatement
    {
        public Type ParentType { get; set; }
        public string Name { get; set; }
    }
}