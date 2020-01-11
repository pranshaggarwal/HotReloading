using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.CodeGenerator.Generators
{
    public class InstanceEventMemberStatementCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var instanceEventMember = (InstanceEventMemberStatement)cSharpSyntax;

            var parentCodeGenerator = CodeGeneratorFactory.Create(instanceEventMember.Parent);

            var parent = parentCodeGenerator.Generate(instanceEventMember.Parent);

            return parent + "." + instanceEventMember.Name;
        }
    }
}
