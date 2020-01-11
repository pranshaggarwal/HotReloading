using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.CodeGenerator.Generators
{
    public class StaticEventMemberStatementCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var staticEventMember = (StaticEventMemberStatement)cSharpSyntax;

            return staticEventMember.ParentType.Name + "." + staticEventMember.Name;
        }
    }
}
