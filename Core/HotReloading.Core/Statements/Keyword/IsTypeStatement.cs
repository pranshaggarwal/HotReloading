namespace HotReloading.Core.Statements
{
    public class IsTypeStatement : Statement
    {
        public Statement Statement { get; set; }
        public Type Type { get; set; }
    }
}
