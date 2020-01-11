using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.CodeGenerator.Generators
{
    public class InstancePropertyMemberStatementCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var instancePropertyMember = (InstancePropertyMemberStatement)cSharpSyntax;

            var parentCodeGenerator = CodeGeneratorFactory.Create(instancePropertyMember.Parent);

            var parent = parentCodeGenerator.Generate(instancePropertyMember.Parent);

            return parent + "." + instancePropertyMember.Name;
        }
    }
}
