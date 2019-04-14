namespace HotReloading.Core.Statements
{
    public abstract class IdentifierStatement : Statement
    {
        public string Name { get; set; }
    }
}