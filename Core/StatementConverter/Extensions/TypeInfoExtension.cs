using HotReloading.Core;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis;

namespace StatementConverter.Extensions
{
    public static class TypeInfoExtension
    {
        public static ClassType GetClassType(this TypeInfo typeInfo)
        {
            var type = typeInfo.Type;
            return GetClassType(type);
        }

        public static ClassType GetClassType(this ITypeSymbol type)
        {
            if (type is INamedTypeSymbol namedTypeSymbol)
                return namedTypeSymbol.GetClassType();
            else if (type is IArrayTypeSymbol arrayTypeSymbol)
                return arrayTypeSymbol.GetClassType();
            else if (type is ITypeParameterSymbol typeParameterSymbol)
                return new ClassType { IsGeneric = true, Name = typeParameterSymbol.Name };
            var typeString = type.Name;
            var containingNamespace = type.ContainingNamespace;
            while (!containingNamespace.IsGlobalNamespace)
            {
                typeString = typeString.Insert(0, containingNamespace.Name + ".");
                containingNamespace = containingNamespace.ContainingNamespace;
            }

            return new ClassType
            {
                Name = typeString,
                AssemblyName = type.ContainingAssembly.Identity.Name
            };
        }

        public static string GetFullyQualifiedName(this ITypeSymbol namedTypeSymbol)
        {
            var typeName = namedTypeSymbol.Name;
            if (namedTypeSymbol.ContainingType != null)
            {
                return GetFullyQualifiedName(namedTypeSymbol.ContainingType) + "+" +
                    typeName;
            }
            else
                return namedTypeSymbol.ContainingNamespace.GetNamespace() + "." +
                    typeName;
        }
    }
}