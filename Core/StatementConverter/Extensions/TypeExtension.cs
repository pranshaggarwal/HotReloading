using System;
using System.Linq;
using System.Reflection;
using HotReloading.Core;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.Extensions
{
    public static class TypeExtension
    {
        public static ClassType GetClassType(this TypeSyntax typeSyntax, SemanticModel semanticModel)
        {
            var typeSymbolInfo = semanticModel.GetSymbolInfo(typeSyntax);
            var typeSymbol = (INamedTypeSymbol)typeSymbolInfo.Symbol;
            return typeSymbol.GetClassType();
        }

        public static PropertyInfo GetMostSuitableProperty(this Type type, string propertyName, BindingFlags bindingFlags)
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

        public static MemberInfo GetMostSuitableMember(this Type type, string memberName, BindingFlags bindingFlags)
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
