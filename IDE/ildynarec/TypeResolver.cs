using System;
using System.Linq;
using System.Collections.Generic;

using Cecil = Mono.Cecil;
using Reflection = System.Reflection;

namespace ILDynaRec
{
    class TypeResolver
    {
        Dictionary<string, Type> typeCache = new Dictionary<string, Type>();

        /// <summary>
        /// Find runtime type in current AppDomain based on Cecil.TypeReference.
        /// </summary>
        /// <param name="typeDef">Cecil type reference.</param>
        /// <returns>Runtime Type.</returns>
        public Type FindType(Cecil.TypeReference typeDef) {
            Type cachedType;
            if (typeCache.TryGetValue(typeDef.FullName, out cachedType)) {
                return cachedType;
            }

            var foundType = LookupType(typeDef);
            typeCache[typeDef.FullName] = foundType;
            return foundType;
        }

        /// <summary>
        /// Find runtime type in current AppDomain based on Cecil.TypeReference.
        /// Do the actual lookup for type
        /// </summary>
        private Type LookupType(Cecil.TypeReference typeDef) {
            if(typeDef.IsByReference) {
                var elemType = FindType(((Cecil.ByReferenceType)typeDef).ElementType);
                return elemType.MakeByRefType();
            }

            if (typeDef.IsGenericInstance) {
                var genericTypeDef = (Cecil.GenericInstanceType)typeDef;
                var ourArgs = genericTypeDef.GenericArguments.Select((arg) => {
                    return FindType(arg);
                }).ToArray();

                var ourBaseType = FindType(genericTypeDef.ElementType);
                return ourBaseType.MakeGenericType(ourArgs);
            }

            if(typeDef.IsGenericParameter) {
                throw new Exception("Unsupported generic parameter");
            }

            string assemblyName;
            try {
                assemblyName = typeDef.FullName + ", " + typeDef.Module.Assembly.FullName;
                assemblyName = assemblyName.Substring(assemblyName.IndexOf(", ") + 2);
                //map our donor assembly to original loaded assembly
                assemblyName = assemblyName.Replace("--hotpatch", "");
            }
            catch (Mono.Cecil.AssemblyResolutionException e) {
                //wtf. cecil bug?
                //sometimes happens with assemblies like System
                assemblyName = e.AssemblyReference.FullName;
            }

            var ourAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(
                (assembly) => assemblyName.StartsWith(assembly.FullName));

            if (ourAssembly == null) {
                return null;
            }

            if (typeDef.DeclaringType != null) {
                var parentType = FindType(typeDef.DeclaringType);
                return parentType.GetNestedType(typeDef.Name,
                    Reflection.BindingFlags.NonPublic | Reflection.BindingFlags.Public);
            }

            var typedefName = typeDef.FullName;
            if (typeDef.IsArray) {
                typedefName = typedefName.Replace("[]", "");
            }

            var ourType = ourAssembly.GetTypes().FirstOrDefault((typ) => typ.FullName == typedefName);
            if (ourType == null) {
                return null;
            }

            if (typeDef.IsArray) {
                return ourType.MakeArrayType();
            }

            return ourType;
        }
    }
}

