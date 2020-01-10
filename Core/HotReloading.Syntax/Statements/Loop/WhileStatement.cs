namespace HotReloading.Syntax.Statements
{
    public class WhileStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Conditional { get; set; }
        public IStatementCSharpSyntax Statement { get; set; }
    }
}
