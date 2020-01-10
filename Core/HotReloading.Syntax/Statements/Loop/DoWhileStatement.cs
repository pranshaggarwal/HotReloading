namespace HotReloading.Syntax.Statements
{
    public class DoWhileStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Conditional { get; set; }
        public IStatementCSharpSyntax Statement { get; set; }
    }
}
