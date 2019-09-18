using HotReloading.Core;
using Microsoft.CodeAnalysis;

namespace StatementConverter.Extensions
{
    public static class ArrayTypeSymbolExtension
    {
        public static HrType GetHrType(this IArrayTypeSymbol type)
        {
            BaseHrType hrType;

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

            if (hrType is HrType hrType1)
                assemblyName = hrType1.AssemblyName;

            return new HrType
            {
                Name = hrType.Name + arrowBraket,
                AssemblyName = assemblyName
            };
        }
    }
}