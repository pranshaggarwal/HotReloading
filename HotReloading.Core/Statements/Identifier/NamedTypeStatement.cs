namespace HotReloading.Core.Statements
{
    public class NamedTypeStatement : IdentifierStatement
    {
        public ClassType Type { get; set; }
    }
}