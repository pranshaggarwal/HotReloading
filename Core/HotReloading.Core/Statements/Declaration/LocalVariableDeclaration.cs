namespace HotReloading.Core.Statements
{
    public class LocalVariableDeclaration : Statement
    {
        public BaseType Type { get; set; }
        public string Name { get; set; }
    }
}