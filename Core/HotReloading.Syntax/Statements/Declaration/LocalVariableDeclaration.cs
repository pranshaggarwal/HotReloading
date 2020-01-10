namespace HotReloading.Syntax.Statements
{
    public class LocalVariableDeclaration : IStatementCSharpSyntax
    {
        public BaseHrType Type { get; set; }
        public string Name { get; set; }
    }
}