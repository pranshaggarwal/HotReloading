namespace HotReloading.Core.Statements
{
    public class UsingStatement : Statement
    {
        public Statement Variable { get; set; }
        public Statement Resource { get; set; }
        public Statement Body { get; set; }
    }
}