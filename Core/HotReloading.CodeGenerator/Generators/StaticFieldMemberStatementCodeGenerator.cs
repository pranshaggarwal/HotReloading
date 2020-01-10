using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.CodeGenerator.Generators
{
    public class StaticFieldMemberStatementCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var staticFieldMember = (StaticFieldMemberStatement)cSharpSyntax;

            return staticFieldMember.ParentType.Name + "." + staticFieldMember.Name;
        }
    }
}
