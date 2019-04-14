namespace HotReloading.Core.Statements
{
    public class InstancePropertyMemberStatement : MemberAccessStatement
    {
        public Statement Parent { get; set; }
        public string Name { get; set; }
    }
}