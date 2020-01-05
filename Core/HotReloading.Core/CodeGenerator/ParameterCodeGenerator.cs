using HotReloading.Core.Statements;
using HotReloading.Syntax;

namespace HotReloading.Core
{
    public class ParameterCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var parameter = (Parameter)cSharpSyntax;

            var parameterStrBuilder = new StringBuilder();

            parameterStrBuilder.Append(parameter.Type.GenerateCode());

            parameterStrBuilder.Append($" {parameter.Name}");

            return parameterStrBuilder.ToString();
        }
    }
}