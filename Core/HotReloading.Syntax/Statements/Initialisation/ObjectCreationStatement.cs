using System.Collections.Generic;

namespace HotReloading.Syntax.Statements
{
    public class ObjectCreationStatement : IStatementCSharpSyntax
    {
        public BaseHrType Type { get; set; }

        public List<IStatementCSharpSyntax> ArgumentList { get; set; }
        public List<IStatementCSharpSyntax> Initializer { get; set; }
        public BaseHrType[] ParametersSignature { get; set; }

        public ObjectCreationStatement()
        {
            Initializer = new List<IStatementCSharpSyntax>();
            ArgumentList = new List<IStatementCSharpSyntax>();
        }
    }
}