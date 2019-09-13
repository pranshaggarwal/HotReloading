namespace HotReloading.Core.Statements
{
    public class CatchStatement : Statement
    {
        public Type Type { get; set; }
        public LocalVariableDeclaration Variable { get; set; }
        public Statement Block { get; set; }
        public Statement Filter { get; set; }
    }
}