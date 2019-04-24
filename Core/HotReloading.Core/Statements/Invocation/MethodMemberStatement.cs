namespace HotReloading.Core.Statements
{
    public class MethodMemberStatement : MemberAccessStatement
    {
        public ClassType ParentType { get; set; }
        public string Name { get; set; }
        public AccessModifier AccessModifier { get; set; }
    }
}