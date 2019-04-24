using Mono.Cecil;

namespace HotReloading.BuildTask.Extensions
{
    public static class FieldDefinitionExtension
    {
        /// <summary>
        /// Gets the reference.
        /// </summary>
        /// <returns>The reference.</returns>
        /// <param name="fieldDefinition">Field definition.</param>
        public static FieldReference GetReference(this FieldDefinition fieldDefinition)
        {
            if (fieldDefinition.DeclaringType.HasGenericParameters)
            {
                var genericInstance = new GenericInstanceType(fieldDefinition.DeclaringType);
                foreach (var parameter in fieldDefinition.DeclaringType.GenericParameters)
                {
                    genericInstance.GenericArguments.Add(parameter);
                }

                return new FieldReference(fieldDefinition.Name, fieldDefinition.FieldType, genericInstance);
            }

            return fieldDefinition;
        }
    }
}