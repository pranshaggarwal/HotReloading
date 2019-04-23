using Mono.Cecil;

namespace HotReloading.BuildTask.Extensions
{
    public static class MethodDefinitionExtension
    {
        /// <summary>
        /// Get reference of base method so that they can be called from child class.
        /// </summary>
        /// <returns>The base reference.</returns>
        /// <param name="methodDefinition">Base Method definition.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="baseType">Base type.</param>
        /// <param name="md">Module Definition.</param>
        public static MethodReference GetBaseReference(this MethodDefinition methodDefinition, TypeReference targetType, TypeReference baseType, ModuleDefinition md)
        {
            if (methodDefinition == null)
                return null;
            var self = md.ImportReference(methodDefinition);
            TypeReference[] arguments = null;

            if (baseType is GenericInstanceType genericInstanceType)
            {
                arguments = genericInstanceType.GenericArguments.ToArray();
            }

            TypeReference declaringType;
            var t = targetType.Resolve();
            if (arguments != null)
                declaringType = self.DeclaringType.MakeGenericType(arguments);
            else
                declaringType = md.ImportReference(self.DeclaringType);
            var reference = new MethodReference(self.Name, self.ReturnType)
            {
                DeclaringType = declaringType,
                HasThis = self.HasThis,
                ExplicitThis = self.ExplicitThis,
                CallingConvention = self.CallingConvention,
            };

            foreach (var parameter in methodDefinition.Parameters)
            {
                var parameterType = parameter.ParameterType.CopyType(targetType, baseType, md, null, false);
                reference.Parameters.Add(new ParameterDefinition(parameterType)
                {
                    IsIn = parameter.IsIn,
                    IsOut = parameter.IsOut,
                    IsOptional = parameter.IsOptional
                });
            }

            foreach (var generic_parameter in methodDefinition.GenericParameters)
                reference.GenericParameters.Add(new GenericParameter(generic_parameter.Name, reference));

            return reference;
        }

        /// <summary>
        /// Gets the reference of method definitions.
        /// </summary>
        /// <returns>The reference.</returns>
        /// <param name="methodDefinition">Method definition.</param>
        /// <param name="currentType">Current type.</param>
        /// <param name="md">Module Definition.</param>
        public static MethodReference GetReference(this MethodDefinition methodDefinition, TypeDefinition currentType, ModuleDefinition md)
        {
            if (methodDefinition == null)
                return null;
            var self = md.ImportReference(methodDefinition);
            TypeReference[] arguments = null;

            if (self.DeclaringType.HasGenericParameters)
            {
                arguments = self.DeclaringType.GenericParameters.ToArray();
            }

            TypeReference declaringType;
            if (arguments != null)
                declaringType = self.DeclaringType.MakeGenericType(arguments);
            else
                declaringType = md.ImportReference(self.DeclaringType);
            var reference = new MethodReference(self.Name, self.ReturnType)
            {
                DeclaringType = declaringType,
                HasThis = self.HasThis,
                ExplicitThis = self.ExplicitThis,
                CallingConvention = self.CallingConvention,
            };

            foreach (var parameter in methodDefinition.Parameters)
            {
                var parameterType = parameter.ParameterType;
                reference.Parameters.Add(new ParameterDefinition(parameterType)
                {
                    IsIn = parameter.IsIn,
                    IsOut = parameter.IsOut,
                    IsOptional = parameter.IsOptional
                });
            }

            foreach (var generic_parameter in methodDefinition.GenericParameters)
                reference.GenericParameters.Add(new GenericParameter(generic_parameter.Name, reference));

            return reference;
        }

    }
}