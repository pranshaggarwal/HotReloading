using HotReloading.Core;
using Microsoft.CodeAnalysis;

namespace StatementConverter.Extensions
{
    public static class ArrayTypeSymbolExtension
    {
        public static Type GetHrType(this IArrayTypeSymbol type)
        {
            BaseType hrType;

            if (type.ElementType is IArrayTypeSymbol)
            {
                hrType = GetHrType((IArrayTypeSymbol)type.ElementType);
            }
            else
                hrType = type.ElementType.GetHrType();

            var arrowBraket = "[";

            for (var i = 1; i < type.Rank; i++)
                arrowBraket += ",";

            arrowBraket += "]";

            string assemblyName = null;

            if (hrType is Type hrType1)
                assemblyName = hrType1.AssemblyName;

            return new Type
            {
                Name = hrType.Name + arrowBraket,
                AssemblyName = assemblyName
            };
        }
    }
}