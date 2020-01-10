using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.CodeGenerator.Generators
{
    public class ConstantStatementCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var constant = (ConstantStatement)cSharpSyntax;

            if (constant.Value == null)
                return "null";

            if (constant.Type == typeof(char))
                return $"'{constant.Value}'";

            if (constant.Type == typeof(string))
                return $"\"{constant.Value}\"";

            return constant.Value.ToString();
        }
    }
}
