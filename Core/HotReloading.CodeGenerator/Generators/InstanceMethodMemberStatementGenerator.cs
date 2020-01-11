using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.CodeGenerator.Generators
{
    public class InstanceMethodMemberStatementCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var instanceMethodMember = (InstanceMethodMemberStatement)cSharpSyntax;

            var parentCodeGenerator = CodeGeneratorFactory.Create(instanceMethodMember.Parent);

            var parent = parentCodeGenerator.Generate(instanceMethodMember.Parent);

            return parent + "." + instanceMethodMember.Name;
        }
    }
}
