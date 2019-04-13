namespace HotReloading.Core.Statements
{
    public class ObjectEqualStatement : Statement
    {
        public Statement Left { get; set; }
        public Statement Right { get; set; }
    }
}