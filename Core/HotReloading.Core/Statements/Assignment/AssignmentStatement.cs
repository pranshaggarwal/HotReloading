using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class AssignmentStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax LeftStatement { get; set; }
        public IStatementCSharpSyntax RightStatement { get; set; }
    }
}