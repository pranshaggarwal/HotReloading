namespace HotReloading.Core.Statements
{
    public class CastStatement : Statement
    {
        public Statement Statement { get; set; }
        public HrType Type { get; set; }
    }
}
