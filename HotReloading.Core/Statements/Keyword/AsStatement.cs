namespace HotReloading.Core.Statements
{
    public class AsStatement : Statement
    {
        public Statement Statement { get; set; }
        public ClassType Type { get; set; }
    }
}
