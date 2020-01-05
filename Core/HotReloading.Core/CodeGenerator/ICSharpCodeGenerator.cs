using HotReloading.Core.Statements;
using HotReloading.Syntax;

namespace HotReloading.Core
{
    public interface ICSharpCodeGenerator
    {
        string Generate(ICSharpSyntax cSharpSyntax);
    }
}