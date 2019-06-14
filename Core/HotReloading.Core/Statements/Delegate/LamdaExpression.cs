namespace HotReloading.Core.Statements
{
    public class LamdaStatement : Statement
    {
        public Parameter[] Parameters { get; set; }
        public Statement Body { get; set; }
        public ClassType Type { get; set; }
        public bool IsAsync { get; set; }
    }
}