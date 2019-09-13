namespace HotReloading.Core.Statements
{
    public class DelegateIdentifierStatement : Statement
    {
        public Statement Target { get; set; }
        public Type Type { get; set; }
    }
}