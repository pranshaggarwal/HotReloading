namespace HotReloading.Core.Statements
{
    public abstract class IdentifierStatement : IStatementCSharpSyntax
    {
        public string Name { get; set; }
    }
}