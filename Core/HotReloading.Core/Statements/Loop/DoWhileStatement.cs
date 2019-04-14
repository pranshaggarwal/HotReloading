namespace HotReloading.Core.Statements
{
    public class DoWhileStatement : Statement
    {
        public Statement Conditional { get; set; }
        public Statement Statement { get; set; }
    }
}
