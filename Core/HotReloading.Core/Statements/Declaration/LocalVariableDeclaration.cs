namespace HotReloading.Core.Statements
{
    public class LocalVariableDeclaration : Statement
    {
        public BaseHrType Type { get; set; }
        public string Name { get; set; }
    }
}