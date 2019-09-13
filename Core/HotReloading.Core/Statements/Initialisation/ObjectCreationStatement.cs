using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class ObjectCreationStatement : Statement
    {
        public Type Type { get; set; }

        public List<Statement> ArgumentList { get; set; }
        public List<Statement> Initializer { get; set; }
        public Type[] ParametersSignature { get; set; }

        public ObjectCreationStatement()
        {
            Initializer = new List<Statement>();
            ArgumentList = new List<Statement>();
        }
    }
}