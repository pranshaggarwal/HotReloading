using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.CodeGenerator.Generators
{
    public class LocalIdentifierStatementCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var localIdentifier = (LocalIdentifierStatement)cSharpSyntax;

            return localIdentifier.Name;
        }
    }
}
