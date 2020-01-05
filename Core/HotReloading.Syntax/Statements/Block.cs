using System.Collections.Generic;

namespace HotReloading.Syntax.Statements
{
    public class Block : IStatementCSharpSyntax
    {
        public Block()
        {
            Statements = new List<IStatementCSharpSyntax>();
        }

        public List<IStatementCSharpSyntax> Statements { get; set; }
    }
}