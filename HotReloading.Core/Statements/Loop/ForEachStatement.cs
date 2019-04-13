namespace HotReloading.Core.Statements
{
    public class ForEachStatement : Statement
    {
        public Statement Variable { get; set; }
        public Statement Collection { get; set; }
        public Statement Body { get; set; }
    }
}
