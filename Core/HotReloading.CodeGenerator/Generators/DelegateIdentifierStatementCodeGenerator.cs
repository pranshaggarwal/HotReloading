using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.CodeGenerator.Generators
{
    public class DelegateIdentifierStatementCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var delegateIdentifier = (DelegateIdentifierStatement)cSharpSyntax;

            var parentCodeGenerator = CodeGeneratorFactory.Create(delegateIdentifier.Target);

            return parentCodeGenerator.Generate(delegateIdentifier.Target);
        }
    }
}
