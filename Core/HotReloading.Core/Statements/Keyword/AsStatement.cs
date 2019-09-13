namespace HotReloading.Core.Statements
{
    public class AsStatement : Statement
    {
        public Statement Statement { get; set; }
        public Type Type { get; set; }
    }
}
