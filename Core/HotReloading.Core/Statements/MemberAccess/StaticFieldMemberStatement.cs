namespace HotReloading.Core.Statements
{
    public class StaticFieldMemberStatement : MemberAccessStatement
    {
        public Type ParentType { get; set; }
        public string Name { get; set; }
    }
}