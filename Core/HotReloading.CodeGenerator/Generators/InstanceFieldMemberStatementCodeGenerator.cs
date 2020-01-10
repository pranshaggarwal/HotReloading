using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.CodeGenerator.Generators
{
    public class InstanceFieldMemberStatementCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var instanceFieldMember = (InstanceFieldMemberStatement)cSharpSyntax;

            var parentCodeGenerator = CodeGeneratorFactory.Create(instanceFieldMember.Parent);

            var parent = parentCodeGenerator.Generate(instanceFieldMember.Parent);

            return parent + "." + instanceFieldMember.Name;
        }
    }
}
