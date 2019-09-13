namespace HotReloading.Core.Statements
{
    public class NamedTypeStatement : IdentifierStatement
    {
        public Type Type { get; set; }
    }
}