using HotReloading.Core;
using HotReloading.Syntax;
using Microsoft.CodeAnalysis;

namespace StatementConverter.Extensions
{
    public static class NamedTypeSymbol
    {
        public static HrType GetHrType(this INamedTypeSymbol namedTypeSymbol)
        {
            string fullyQualifiedName = namedTypeSymbol.GetFullyQualifiedName();
            if(namedTypeSymbol.IsGenericType)
            {

                fullyQualifiedName += "`" + namedTypeSymbol.TypeArguments.Length + "[";

                foreach (var argument in namedTypeSymbol.TypeArguments)
                {
                    fullyQualifiedName += "[" + argument.GetHrType().AssemblyQualifiedName + "], ";
                }

                fullyQualifiedName = fullyQualifiedName.Remove(fullyQualifiedName.Length - 2) + "]";
            }

            return new HrType
            {
                Name = fullyQualifiedName,
                AssemblyName = namedTypeSymbol.ContainingAssembly.Identity.Name
            };
        }



        public static string GetNamespace(this INamespaceSymbol namespaceSymbol)
        {
            string @namespace = "";
            var containingNamespace = namespaceSymbol;
            while (!containingNamespace.IsGlobalNamespace)
            {
                @namespace = @namespace.Insert(0, containingNamespace.Name + ".");
                containingNamespace = containingNamespace.ContainingNamespace;
            }

            @namespace = @namespace.Remove(@namespace.Length - 1);

            return @namespace;
        }
    }
}