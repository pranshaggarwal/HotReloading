namespace HotReloading.Core.Statements
{
    public class LamdaStatement : IStatementCSharpSyntax
    {
        public Parameter[] Parameters { get; set; }
        public IStatementCSharpSyntax Body { get; set; }
        public BaseHrType Type { get; set; }
        public bool IsAsync { get; set; }
    }
}