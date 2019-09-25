namespace HotReloading.Core.Statements
{
    public class ForEachStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Variable { get; set; }
        public IStatementCSharpSyntax Collection { get; set; }
        public IStatementCSharpSyntax Body { get; set; }
    }
}
