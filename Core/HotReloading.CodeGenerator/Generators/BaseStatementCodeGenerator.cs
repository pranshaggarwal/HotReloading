using HotReloading.Syntax;

namespace HotReloading.CodeGenerator.Generators
{
    public class BaseStatementCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            return "base";
        }
    }
}
