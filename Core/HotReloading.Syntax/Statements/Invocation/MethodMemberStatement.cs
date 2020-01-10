namespace HotReloading.Syntax.Statements
{
    public class MethodMemberStatement : MemberAccessStatement
    {
        public HrType ParentType { get; set; }
        public string Name { get; set; }
    }
}