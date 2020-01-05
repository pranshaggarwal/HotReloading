using System;
using HotReloading.Syntax;

namespace HotReloading.CodeGenerator.Generators
{
    public interface ICSharpCodeGenerator
    {
        string Generate(ICSharpSyntax cSharpSyntax);
    }
}
