namespace HotReloading.Core.Statements
{
    public class LocalVariableDeclaration : IStatementCSharpSyntax
    {
        public BaseHrType Type { get; set; }
        public string Name { get; set; }
    }
}