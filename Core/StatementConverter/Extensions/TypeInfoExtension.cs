using HotReloading.Core;
using HotReloading.Core.Statements;
using HotReloading.Syntax;
using Microsoft.CodeAnalysis;

namespace StatementConverter.Extensions
{
    public static class TypeInfoExtension
    {
        public static BaseHrType GetHrType(this TypeInfo typeInfo)
        {
            var type = typeInfo.Type;
            return GetHrType(type);
        }

        public static BaseHrType GetHrType(this ITypeSymbol type)
        {
            if (type is INamedTypeSymbol namedTypeSymbol)
                return namedTypeSymbol.GetHrType();
            else if (type is IArrayTypeSymbol arrayTypeSymbol)
                return arrayTypeSymbol.GetHrType();
            else if (type is ITypeParameterSymbol typeParameterSymbol)
                return new GenericHrType { Name = typeParameterSymbol.Name };
            var typeString = type.Name;
            var containingNamespace = type.ContainingNamespace;
            while (!containingNamespace.IsGlobalNamespace)
            {
                typeString = typeString.Insert(0, containingNamespace.Name + ".");
                containingNamespace = containingNamespace.ContainingNamespace;
            }

            return new HrType
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

        public static bool IsAssignableTo(this ITypeSymbol type, ITypeSymbol targetType)
        {
            if (type == null)
                return false;
            if(type.GetFullyQualifiedName() == targetType.GetFullyQualifiedName())
            {
                return true;
            }

            return type.BaseType.IsAssignableTo(targetType);
        }

        public static bool IsSubClassOf(this ITypeSymbol type, ITypeSymbol targetType)
        {
            return type.BaseType.IsAssignableTo(targetType);
        }
    }
}