using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class Block : Statement
    {
        public Block()
        {
            Statements = new List<Statement>();
        }

        public List<Statement> Statements { get; set; }
    }
}