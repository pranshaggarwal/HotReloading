using System.Text;
using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.CodeGenerator.Generators
{
    public class DelegateInvocationStatementCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var delegateInvocationStatement = (DelegateInvocationStatement)cSharpSyntax;

            var identifierCodeGenerator = CodeGeneratorFactory.Create(delegateInvocationStatement.Delegate);

            var codeStrBuilder = new StringBuilder();
            codeStrBuilder.Append(identifierCodeGenerator.Generate(delegateInvocationStatement.Delegate));

            codeStrBuilder.Append("(");

            bool isNotFirstArgument = false;
            foreach (var argument in delegateInvocationStatement.Arguments)
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
