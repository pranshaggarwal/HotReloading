using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.CodeGenerator.Generators
{
    public class StaticMethodMemberStatementCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var staticMethodMember = (StaticMethodMemberStatement)cSharpSyntax;

            return staticMethodMember.ParentType.Name + "." + staticMethodMember.Name;
        }
    }
}
