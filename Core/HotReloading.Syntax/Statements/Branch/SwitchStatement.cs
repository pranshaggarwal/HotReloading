namespace HotReloading.Syntax.Statements
{
    public class SwitchStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax SwitchValue { get; set; }
        public IStatementCSharpSyntax Default { get; set; }
        public SwitchCase[] Cases { get; set; }
    }
}
