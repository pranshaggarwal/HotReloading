using System;
using HotReloading.CodeGenerator.Generators;
using HotReloading.Syntax;

namespace HotReloading.CodeGenerator
{
    public static class CodeGeneratorFactory
    {
        public static ICSharpCodeGenerator Create(ICSharpSyntax cSharpSyntax)
        {
            var generatorNamespace = typeof(ICSharpCodeGenerator).Namespace;
            var syntaxType = cSharpSyntax.GetType();
            var generatorName = generatorNamespace + "." + syntaxType.Name + "CodeGenerator";
            var generatorType = typeof(ICSharpCodeGenerator).Assembly.GetType(generatorName);
            var generator = Activator.CreateInstance(generatorType);

            if (generator == null)
                throw new Exception("Generator not found for " + syntaxType.Name);

            return (ICSharpCodeGenerator)generator;
        }
    }
}
