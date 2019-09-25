namespace HotReloading.Core.Statements
{
    public class UsingStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Variable { get; set; }
        public IStatementCSharpSyntax Resource { get; set; }
        public IStatementCSharpSyntax Body { get; set; }
    }
}