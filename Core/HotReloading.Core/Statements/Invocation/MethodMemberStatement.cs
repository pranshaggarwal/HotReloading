namespace HotReloading.Core.Statements
{
    public class MethodMemberStatement : MemberAccessStatement
    {
        public Type ParentType { get; set; }
        public string Name { get; set; }
    }
}