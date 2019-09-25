namespace HotReloading.Core.Statements
{
    public class DoWhileStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Conditional { get; set; }
        public IStatementCSharpSyntax Statement { get; set; }
    }
}
