namespace HotReloading.Core.Statements
{
    public class LocalVariableDeclaration : Statement
    {
        public Type Type { get; set; }
        public string Name { get; set; }
    }
}