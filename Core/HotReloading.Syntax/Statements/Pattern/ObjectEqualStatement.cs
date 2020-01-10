namespace HotReloading.Syntax.Statements
{
    public class ObjectEqualStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Left { get; set; }
        public IStatementCSharpSyntax Right { get; set; }
    }
}