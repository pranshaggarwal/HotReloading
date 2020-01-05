using System.Text;
using HotReloading.Core.Statements;

namespace HotReloading.Core
{
    public interface ICSharpCodeGenerator
    {
        string Generate(ICSharpSyntax cSharpSyntax);
    }

    public static class CodeGeneratorFactory
    {
        public static ICSharpCodeGenerator Create(ICSharpSyntax cSharpSyntax)
        {
            var type = cSharpSyntax.GetType();
            var name = type.FullName + "CodeGenerator";
            var generatorType = type.Assembly.GetType(name);
            var generator = Activator.CreateInstance(generatorType);

            if (generator == null)
                throw new Exception("Generator not found for " + type.Name);

            return (ICSharpCodeGenerator)generator;
        }
    }

    public static class BaseHrTypeExtensions
    {
        public static string GenerateCode(this BaseHrType hrType)
        {
            var code = hrType.Name;
            if (code == "System.Void")
                code = "void";

            return code;
        }
    }

    public static class AccessModifierExtensions
    {
        public static string GenerateCode(this AccessModifier accessModifier)
        {
            switch (accessModifier)
            {
                case AccessModifier.ProtectedInternal:
                    return "protected internal";
                default:
                    return accessModifier.ToString().ToLower();
            }
        }
    }

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
                        methodStrBuilder.Append($"\t{statementFactory.Generate(statement)}");
                    }
                }

                methodStrBuilder.Append("\n}");
            }

            return methodStrBuilder.ToString();
        }
    }
}