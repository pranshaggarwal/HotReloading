namespace HotReloading.Core.Statements
{
    public class DelegateIdentifierStatement : Statement
    {
        public Statement Target { get; set; }
        public BaseType Type { get; set; }
    }
}