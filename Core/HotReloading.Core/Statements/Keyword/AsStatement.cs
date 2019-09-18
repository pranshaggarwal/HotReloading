namespace HotReloading.Core.Statements
{
    public class AsStatement : Statement
    {
        public Statement Statement { get; set; }
        public HrType Type { get; set; }
    }
}
