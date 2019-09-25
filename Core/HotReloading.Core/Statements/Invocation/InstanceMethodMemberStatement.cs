namespace HotReloading.Core.Statements
{
    public class InstanceMethodMemberStatement : MethodMemberStatement
    {
        public IStatementCSharpSyntax Parent { get; set; }
    }
}