namespace HotReloading.Core.Statements
{
    public class MethodPointerStatement : Statement
    {
        public Statement Method { get; set; }
        public BaseType Type { get; set; }
    }
}