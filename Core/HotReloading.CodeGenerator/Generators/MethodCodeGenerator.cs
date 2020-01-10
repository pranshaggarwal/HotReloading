using System.Linq;
using System.Text;
using HotReloading.CodeGenerator.Extensions;
using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.CodeGenerator.Generators
{
    public class MethodCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var method = (Method)cSharpSyntax;

            var methodStrBuilder = new StringBuilder();
            methodStrBuilder.Append(method.AccessModifier.GenerateCode());

            if (method.IsStatic)
                methodStrBuilder.Append(" static");

            if (method.IsSealed)
                methodStrBuilder.Append(" sealed");

            if (method.IsVirtual)
                methodStrBuilder.Append(" virtual");

            if (method.IsAbstract)
                methodStrBuilder.Append(" abstract");

            if (method.IsOverrided)
                methodStrBuilder.Append(" override");

            if (method.IsAsync)
                methodStrBuilder.Append(" async");

            methodStrBuilder.Append($" {method.ReturnType.GenerateCode()}");

            methodStrBuilder.Append($" {method.Name}(");

            if (method.Parameters.Any())
            {
                var parameterCodeGenerator = CodeGeneratorFactory.Create(method.Parameters[0]);
                var parameters = method.Parameters.Aggregate("", (lastResult, parameter) =>
                {
                    if (!string.IsNullOrWhiteSpace(lastResult))
                    {
                        lastResult += ", ";
                    }
                    return lastResult + parameterCodeGenerator.Generate(parameter);
                });

                methodStrBuilder.Append(parameters);
            }

            methodStrBuilder.Append(")");

            if (method.IsAbstract)
            {
                methodStrBuilder.Append(";");
            }
            else
            {
                methodStrBuilder.Append("\n{");

                if (method.Body != null)
                {
                    foreach (var statement in method.Body.Statements)
                    {
                        var statementFactory = CodeGeneratorFactory.Create(statement);
                        methodStrBuilder.Append($"\n\t{statementFactory.Generate(statement)}");
                    }
                }

                methodStrBuilder.Append("\n}");
            }

            return methodStrBuilder.ToString();
        }
    }
}
