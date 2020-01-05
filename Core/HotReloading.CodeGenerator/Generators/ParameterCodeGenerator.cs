using System.Text;
using HotReloading.CodeGenerator.Extensions;
using HotReloading.Syntax;

namespace HotReloading.CodeGenerator.Generators
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
