using System;
using System.Linq;
using Mono.Cecil;

namespace HotReloading.BuildTask.Extensions
{
    public static class TypeReferenceExtension
    {
        /// <summary>
        /// Copies the type from one class to other.
        /// </summary>
        /// <returns>Converted Type.</returns>
        /// <param name="self">Type that need to be converted.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="sourceType">Source type.</param>
        /// <param name="md">ModuleDefinition</param>
        /// <param name="targetMethod">Target method.</param>
        /// <param name="shouldResolveGenericParaemeter">If set to <c>true</c> should resolve generic paraemeter.</param>
        public static TypeReference CopyType(this TypeReference self, TypeReference targetType, TypeReference sourceType, ModuleDefinition md, MethodReference targetMethod = null, bool shouldResolveGenericParaemeter = true)
        {
            if (self is GenericParameter genericParameter)
            {
                if (!shouldResolveGenericParaemeter)
                    return self;
                if (genericParameter.Type == GenericParameterType.Method)
                {
                    return targetMethod.GenericParameters[genericParameter.Position];
                }

                var baseTypeDefinition = sourceType.Resolve();

                var baseGenericParameter = baseTypeDefinition.GenericParameters.FirstOrDefault(x => x.Name == genericParameter.Name);

                if (baseGenericParameter != null)
                {
                    var genericInstanceType = (GenericInstanceType)sourceType;
                    var genericArgument = genericInstanceType.GenericArguments[genericParameter.Position];
                    if (genericArgument.IsGenericParameter)
                    {
                        return genericArgument;
                    }
                    else
                    {
                        return genericArgument.CopyType(targetType, sourceType, md, targetMethod, shouldResolveGenericParaemeter);
                    }
                }
                else
                    return targetType.GenericParameters.FirstOrDefault(x => x.Name == genericParameter.Name) ?? self;
            }
            else
            {
                if (self is GenericInstanceType genericInstanceType)
                {
                    var elementType = md.ImportReference(genericInstanceType.ElementType);
                    var arguments = genericInstanceType.GenericArguments.Select(x => x.CopyType(targetType, sourceType, md, targetMethod, shouldResolveGenericParaemeter)).ToArray();
                    return elementType.MakeGenericType(arguments);
                }
                else if (self is ByReferenceType byReferenceType && targetType != null)
                {
                    var elementType = byReferenceType.ElementType.CopyType(targetType, sourceType, md, targetMethod, shouldResolveGenericParaemeter);
                    return new ByReferenceType(elementType);
                }

                return md.ImportReference(self);
            }
        }

        /// <summary>
        /// Makes generic type by adding its argument.
        /// </summary>
        /// <returns>The generic type.</returns>
        /// <param name="self">Type that need to be converted.</param>
        /// <param name="arguments">Generic Arguments.</param>
        public static TypeReference MakeGenericType(this TypeReference self, params TypeReference[] arguments)
        {
            if (self.GenericParameters.Count != arguments.Length)
                throw new ArgumentException();

            var instance = new GenericInstanceType(self);
            foreach (var argument in arguments)
            {
                instance.GenericArguments.Add(argument);
            }

            return instance;
        }
    }
}