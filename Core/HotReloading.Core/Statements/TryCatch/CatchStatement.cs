namespace HotReloading.Core.Statements
{
    public class CatchStatement : Statement
    {
        public BaseHrType Type { get; set; }
        public LocalVariableDeclaration Variable { get; set; }
        public Statement Block { get; set; }
        public Statement Filter { get; set; }
    }
}