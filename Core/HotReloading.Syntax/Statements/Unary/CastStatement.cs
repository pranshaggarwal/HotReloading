namespace HotReloading.Syntax.Statements
{
    public class CastStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Statement { get; set; }
        public HrType Type { get; set; }
    }
}
