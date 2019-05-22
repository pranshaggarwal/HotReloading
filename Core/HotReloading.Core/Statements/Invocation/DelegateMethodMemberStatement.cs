namespace HotReloading.Core.Statements
{
    public class DelegateMethodMemberStatement : MethodMemberStatement
    {
        public Statement Delegate { get; set; }
    }
}