using HotReloading.Core.Statements;

namespace HotReloading.Core
{
    public interface ICSharpCodeGenerator
    {
        string Generate(ICSharpSyntax cSharpSyntax);
    }
}