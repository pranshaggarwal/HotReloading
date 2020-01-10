namespace HotReloading.Syntax.Statements
{
    public class IsTypeStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Statement { get; set; }
        public HrType Type { get; set; }
    }
}
