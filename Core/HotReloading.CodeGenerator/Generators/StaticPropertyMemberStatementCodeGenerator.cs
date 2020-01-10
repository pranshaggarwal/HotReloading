using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.CodeGenerator.Generators
{
    public class StaticPropertyMemberStatementCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var staticPropertyMember = (StaticPropertyMemberStatement)cSharpSyntax;

            return staticPropertyMember.ParentType.Name + "." + staticPropertyMember.Name;
        }
    }
}
