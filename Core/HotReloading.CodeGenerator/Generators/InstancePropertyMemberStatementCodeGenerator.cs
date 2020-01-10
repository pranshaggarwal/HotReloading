using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.CodeGenerator.Generators
{
    public class InstancePropertyMemberStatementCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var instancePropertuMember = (InstancePropertyMemberStatement)cSharpSyntax;

            var parentCodeGenerator = CodeGeneratorFactory.Create(instancePropertuMember.Parent);

            var parent = parentCodeGenerator.Generate(instancePropertuMember.Parent);

            return parent + "." + instancePropertuMember.Name;
        }
    }
}
