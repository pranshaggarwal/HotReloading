using System;
using System.Linq;
using System.Reflection;
using HotReloading.Core;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using HrType = HotReloading.Core.HrType;

namespace StatementConverter.Extensions
{
    public static class TypeExtension
    {
        public static HrType GetClassType(this TypeSyntax typeSyntax, SemanticModel semanticModel)
        {
            var typeSymbolInfo = semanticModel.GetSymbolInfo(typeSyntax);
            var typeSymbol = (INamedTypeSymbol)typeSymbolInfo.Symbol;
            return typeSymbol.GetHrType();
        }

        public static PropertyInfo GetMostSuitableProperty(this System.Type type, string propertyName, BindingFlags bindingFlags)
        {
            var properties = type.GetProperties(bindingFlags).Where(x => x.Name == propertyName);

            var derivedType = properties.FirstOrDefault().DeclaringType;

            foreach (var property in properties)
            {
                if (property.DeclaringType.IsSubclassOf(derivedType))
                {
                    derivedType = property.DeclaringType;
                }
            }

            return properties.Single(x => x.DeclaringType == derivedType);
        }

        public static MemberInfo GetMostSuitableMember(this System.Type type, string memberName, BindingFlags bindingFlags)
        {
            var members = type.GetMembers(bindingFlags).Where(x => x.Name == memberName);

            var derivedType = members.FirstOrDefault().DeclaringType;

            foreach (var member in members)
            {
                if (member.DeclaringType.IsSubclassOf(derivedType))
                {
                    derivedType = member.DeclaringType;
                }
            }

            return members.Single(x => x.DeclaringType == derivedType);
        }
    }
}
