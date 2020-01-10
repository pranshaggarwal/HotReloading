using HotReloading.Syntax;

namespace HotReloading.CodeGenerator.Generators
{
    public class ThisStatementCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            return "this";
        }
    }
}
