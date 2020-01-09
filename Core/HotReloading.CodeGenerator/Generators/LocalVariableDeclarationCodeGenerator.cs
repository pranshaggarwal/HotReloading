using HotReloading.CodeGenerator.Extensions;
using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.CodeGenerator.Generators
{
    public class LocalVariableDeclarationCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var localVariableDeclaration = (LocalVariableDeclaration)cSharpSyntax;

            return $"{localVariableDeclaration.Type.GenerateCode()} {localVariableDeclaration.Name};";
        }
    }
}
