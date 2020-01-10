using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.CodeGenerator.Generators
{
    public class NamedTypeStatementCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var namedType = (NamedTypeStatement)cSharpSyntax;

            return namedType.Type.Name;
        }
    }
}
