namespace HotReloading.Core.Statements
{
    public class InstanceFieldMemberStatement : MemberAccessStatement
    {
        public Statement Parent { get; set; }
        public string Name { get; set; }
    }
}