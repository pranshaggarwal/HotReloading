namespace HotReloading.Core.Statements
{
    public class MethodMemberStatement : MemberAccessStatement
    {
        public HrType ParentType { get; set; }
        public string Name { get; set; }
    }
}