namespace HotReloading.Syntax.Statements
{
    public class CatchStatement : IStatementCSharpSyntax
    {
        public BaseHrType Type { get; set; }
        public LocalVariableDeclaration Variable { get; set; }
        public IStatementCSharpSyntax Block { get; set; }
        public IStatementCSharpSyntax Filter { get; set; }
    }
}