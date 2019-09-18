using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class ObjectCreationStatement : Statement
    {
        public BaseHrType Type { get; set; }

        public List<Statement> ArgumentList { get; set; }
        public List<Statement> Initializer { get; set; }
        public BaseHrType[] ParametersSignature { get; set; }

        public ObjectCreationStatement()
        {
            Initializer = new List<Statement>();
            ArgumentList = new List<Statement>();
        }
    }
}