namespace HotReloading.Core.Statements
{
    public class CastStatement : Statement
    {
        public Statement Statement { get; set; }
        public ClassType Type { get; set; }
    }
}
