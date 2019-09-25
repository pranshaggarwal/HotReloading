namespace HotReloading.Core.Statements
{
    public class DelegateObjectCreationStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Method { get; set; }
    }
}