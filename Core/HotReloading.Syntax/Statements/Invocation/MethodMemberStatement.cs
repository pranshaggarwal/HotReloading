namespace HotReloading.Syntax.Statements
{
    public abstract class MethodMemberStatement : MemberAccessStatement
    {
        public HrType ParentType { get; set; }
        public string Name { get; set; }
    }
}