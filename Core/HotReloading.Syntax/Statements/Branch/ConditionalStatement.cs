namespace HotReloading.Syntax.Statements
{
    public class ConditionalStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Condition { get; set; }
        public IStatementCSharpSyntax IfTrue { get; set; }
        public IStatementCSharpSyntax IfFalse { get; set; }
    }
}
