namespace HotReloading.Syntax.Statements
{
    public class IfStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Condition { get; set; }
        public IStatementCSharpSyntax IfTrue { get; set; }
        public IStatementCSharpSyntax IfFalse { get; set; }
    }
}
