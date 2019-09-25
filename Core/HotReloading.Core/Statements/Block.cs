using System.Collections.Generic;

namespace HotReloading.Core.Statements
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