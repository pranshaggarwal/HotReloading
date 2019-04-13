namespace HotReloading.Core.Statements
{
    public class SwitchCase
    {
        public Statement Body { get; set; }
        public Statement[] Tests { get; set; }
    }
}
