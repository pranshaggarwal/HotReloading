namespace HotReloading.Core.Statements
{
    public class IsTypeStatement : Statement
    {
        public Statement Statement { get; set; }
        public ClassType Type { get; set; }
    }
}
