namespace HotReloading.Core.Statements
{
    public class AssignmentStatement : Statement
    {
        public Statement LeftStatement { get; set; }
        public Statement RightStatement { get; set; }
    }
}