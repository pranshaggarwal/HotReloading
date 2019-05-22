namespace HotReloading.Core.Statements
{
    public class DelegateIdentifierStatement : Statement
    {
        public Statement Target { get; set; }
        public ClassType Type { get; set; }
    }
}