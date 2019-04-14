namespace HotReloading.Core.Statements
{
    public class WhileStatement : Statement
    {
        public Statement Conditional { get; set; }
        public Statement Statement { get; set; }
    }
}
