using System;
using HotReloading.Core.Statements;
using HotReloading.Syntax;

namespace HotReloading.Core
{
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
}