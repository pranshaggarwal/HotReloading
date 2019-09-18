namespace HotReloading.Core.Statements
{
    public class IsTypeStatement : Statement
    {
        public Statement Statement { get; set; }
        public HrType Type { get; set; }
    }
}
