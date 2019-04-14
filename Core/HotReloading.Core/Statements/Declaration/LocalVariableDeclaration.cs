namespace HotReloading.Core.Statements
{
    public class LocalVariableDeclaration : Statement
    {
        public ClassType Type { get; set; }
        public string Name { get; set; }
    }
}