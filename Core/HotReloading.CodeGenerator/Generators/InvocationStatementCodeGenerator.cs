using System.Text;
using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.CodeGenerator.Generators
{
    public class InvocationStatementCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var invocationStatement = (InvocationStatement)cSharpSyntax;

            var methodCodeGenerator = CodeGeneratorFactory.Create(invocationStatement.Method);

            var codeStrBuilder = new StringBuilder();
            codeStrBuilder.Append(methodCodeGenerator.Generate(invocationStatement.Method));

            codeStrBuilder.Append("(");

            bool isNotFirstArgument = false;
            foreach (var argument in invocationStatement.Arguments)
            {
                if (isNotFirstArgument)
                    codeStrBuilder.Append(", ");
                isNotFirstArgument = true;

                var argumentCodeGenerator = CodeGeneratorFactory.Create(argument);

                codeStrBuilder.Append(argumentCodeGenerator.Generate(argument));
            }

            codeStrBuilder.Append(")");

            return codeStrBuilder.ToString();
        }
    }
}
