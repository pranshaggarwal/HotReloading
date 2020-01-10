namespace HotReloading.Syntax.Statements
{
    public class MethodPointerStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Method { get; set; }
        public BaseHrType Type { get; set; }
    }
}