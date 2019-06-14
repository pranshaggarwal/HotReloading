using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class ObjectCreationStatement : Statement
    {
        public ClassType Type { get; set; }

        public List<Statement> ArgumentList { get; set; }
        public List<Statement> Initializer { get; set; }
        public ClassType[] ParametersSignature { get; set; }

        public ObjectCreationStatement()
        {
            Initializer = new List<Statement>();
            ArgumentList = new List<Statement>();
        }
    }
}