namespace HotReloading.Core.Statements
{
    public class MethodPointerStatement : Statement
    {
        public Statement Method { get; set; }
        public Type Type { get; set; }
    }
}