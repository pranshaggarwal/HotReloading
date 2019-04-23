using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace HotReloading.BuildTask.Extensions
{
    public static class TypeDefinitionExtension
    {
        public static bool IsDelegate(this TypeDefinition typeDefinition, ModuleDefinition md)
        {
            var multiCastDelegate = md.ImportReference(typeof(MulticastDelegate));
            if (multiCastDelegate.FullName == typeDefinition.BaseType.FullName)
            {
                return true;
            }
            return false;
        }

        public static bool HasImplementedIInstanceClass(this TypeDefinition typeDefinition, TypeReference iInstanceClassType, out MethodDefinition getInstanceMethod, out MethodDefinition instanceMethodGetters)
        {
            if (typeDefinition.Interfaces.Any(x => x.InterfaceType.FullName == iInstanceClassType.FullName))
            {
                getInstanceMethod = typeDefinition.Methods.FirstOrDefault(x => x.Name == "GetInstanceMethod");
                instanceMethodGetters = typeDefinition.Methods.FirstOrDefault(x => x.Name == "get_InstanceMethods");
                return true;
            }
            if (typeDefinition.BaseType is TypeDefinition baseTypeDefinition)
                return baseTypeDefinition.HasImplementedIInstanceClass(iInstanceClassType, out getInstanceMethod, out instanceMethodGetters);

            getInstanceMethod = null;
            instanceMethodGetters = null;
            return false;
        }

        public static PropertyDefinition CreateProperty(this TypeDefinition typeDefinition, 
            string propertyName,
            TypeReference retType,
            MethodDefinition getter, 
            MethodDefinition setter)
        {
            var property = new PropertyDefinition(propertyName, PropertyAttributes.None, retType)
            {
                GetMethod = getter,
                SetMethod = setter
            };

            typeDefinition.Properties.Add(property);

            return property;
        }

        public static PropertyDefinition CreateAutoProperty(this TypeDefinition typeDefinition,
            ModuleDefinition moduleDefinition, string propertyName, TypeReference retType, bool vir = false)
        {
            var field = typeDefinition.CreateAutoPropertyField(moduleDefinition, propertyName, retType);

            var getter = typeDefinition.CreateAutoPropertyGetter(moduleDefinition, propertyName, field, retType, vir);


            var setter = typeDefinition.CreateAutoPropertySetter(moduleDefinition, propertyName, field, retType, vir);

            return typeDefinition.CreateProperty(propertyName, retType, getter, setter);
        }

        public static FieldDefinition CreateField(this TypeDefinition typeDefinition, ModuleDefinition moduleDefinition, string fieldName,
            TypeReference typeReference)
        {
            var field = new FieldDefinition(fieldName, FieldAttributes.Private, typeReference);

            typeDefinition.Fields.Add(field);
            return field;
        }

        private static FieldDefinition CreateAutoPropertyField(this TypeDefinition typeDefinition, ModuleDefinition moduleDefinition, string propertyName,
            TypeReference typeReference)
        {
            var field = typeDefinition.CreateField(moduleDefinition, $"<{propertyName}>k__BackingField", typeReference);
            field.CustomAttributes.Add(new CustomAttribute(
                moduleDefinition.ImportReference(typeof(CompilerGeneratedAttribute).GetConstructor(Type.EmptyTypes))));
            return field;
        }

        public static MethodDefinition CreateGetter(this TypeDefinition typeDefinition, ModuleDefinition moduleDefinition, string propertyName,
            TypeReference retTypeReference, List<Instruction> instructions, MethodAttributes attributes = MethodAttributes.Public, bool vir = false)
        {
            if (vir)
                attributes |= MethodAttributes.NewSlot | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.Final | MethodAttributes.FamANDAssem | MethodAttributes.Family;

            var getter = new MethodDefinition($"get_{propertyName}", attributes, retTypeReference);

            var getIlProcessor = getter.Body.GetILProcessor();
            foreach (var getInstruction in instructions) getIlProcessor.Append(getInstruction);

            typeDefinition.Methods.Add(getter);

            return getter;
        }

        private static MethodDefinition CreateAutoPropertyGetter(this TypeDefinition typeDefinition, ModuleDefinition moduleDefinition, string propertyName,
            FieldDefinition field, TypeReference retTypeReference, bool vir = false)
        {
            var attributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig |
                             MethodAttributes.SpecialName;
            var composer = new InstructionComposer(moduleDefinition);
            composer.AddAutoPropertyGetterInstruction(field);

            var getter = typeDefinition.CreateGetter(moduleDefinition, propertyName, retTypeReference, composer.Instructions, attributes, vir);

            getter.CustomAttributes.Add(new CustomAttribute(
                moduleDefinition.ImportReference(typeof(CompilerGeneratedAttribute).GetConstructor(Type.EmptyTypes))));

            return getter;
        }

        private static MethodDefinition CreateSetter(this TypeDefinition typeDefinition, 
            ModuleDefinition moduleDefinition, 
            string propertyName,
            FieldDefinition field, 
            TypeReference retTypeReference, 
            List<Instruction> instructions, 
            MethodAttributes attributes = MethodAttributes.Public, 
            bool vir = false)
        {
            if (vir)
                attributes |= MethodAttributes.NewSlot | MethodAttributes.Virtual;

            var setter = new MethodDefinition($"set_{propertyName}", attributes,
                moduleDefinition.ImportReference(typeof(void)));

            var value = new ParameterDefinition("value", ParameterAttributes.None, retTypeReference);

            setter.Parameters.Add(value);

            var setIlProcessor = setter.Body.GetILProcessor();
            foreach (var getInstruction in instructions) setIlProcessor.Append(getInstruction);

            typeDefinition.Methods.Add(setter);

            return setter;
        }


        private static MethodDefinition CreateAutoPropertySetter(this TypeDefinition typeDefinition, ModuleDefinition moduleDefinition, string propertyName,
            FieldDefinition field, TypeReference retTypeReference, bool vir = false)
        {
            var attributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig |
                             MethodAttributes.SpecialName;

            var composer = new InstructionComposer(moduleDefinition);
            composer.AddAutoPropertySetterInstruction(field);

            var setter = typeDefinition.CreateSetter(moduleDefinition, propertyName, field, retTypeReference,
                composer.Instructions, attributes, vir);

            setter.CustomAttributes.Add(new CustomAttribute(
                moduleDefinition.ImportReference(typeof(CompilerGeneratedAttribute).GetConstructor(Type.EmptyTypes))));

            return setter;
        }

        public static FieldReference GetReference(this FieldDefinition fieldDefinition)
        {
            if(fieldDefinition.DeclaringType.HasGenericParameters)
            {
                var genericInstance = new GenericInstanceType(fieldDefinition.DeclaringType);
                foreach(var parameter in fieldDefinition.DeclaringType.GenericParameters)
                {
                    genericInstance.GenericArguments.Add(parameter);
                }

                return new FieldReference(fieldDefinition.Name, fieldDefinition.FieldType, genericInstance);
            }

            return fieldDefinition;
        }

        public static MethodReference GetReference(this MethodReference source, TypeDefinition currentType, TypeReference targetType, ModuleDefinition md, bool shouldResolveGenericParameter = true)
        {
            if (source == null)
                return null;
            var self = md.ImportReference(source);
            TypeReference[] arguments = null;

            if(currentType == targetType)
            {
                if (self.DeclaringType.HasGenericParameters)
                {
                    arguments = self.DeclaringType.GenericParameters.ToArray();
                }
            }
            else if(currentType.BaseType == targetType)
            {
                if (currentType.BaseType is GenericInstanceType genericInstanceType)
                {
                    arguments = genericInstanceType.GenericArguments.ToArray();
                }
            }

            TypeReference declaringType;
            if (arguments != null)
                declaringType = self.DeclaringType.MakeGenericType(md, currentType, shouldResolveGenericParameter, arguments);
            else
                declaringType = md.ImportReference(self.DeclaringType);
            var reference = new MethodReference(self.Name, self.ReturnType)
            {
                DeclaringType = declaringType,
                HasThis = self.HasThis,
                ExplicitThis = self.ExplicitThis,
                CallingConvention = self.CallingConvention,
            };

            foreach (var parameter in source.Parameters)
            {
                var parameterType = parameter.ParameterType.CopyType(md, currentType, null, shouldResolveGenericParameter);
                reference.Parameters.Add(new ParameterDefinition(parameterType)
                {
                    IsIn = parameter.IsIn,
                    IsOut = parameter.IsOut,
                    IsOptional = parameter.IsOptional
                });
            }

            foreach (var generic_parameter in source.GenericParameters)
                reference.GenericParameters.Add(new GenericParameter(generic_parameter.Name, reference));

            return reference;
        }

        public static TypeReference MakeGenericType(this TypeReference self, ModuleDefinition md, TypeDefinition targetType, bool shouldResolveGenericParameter, params TypeReference[] arguments)
        {
            if (self.GenericParameters.Count != arguments.Length)
                throw new ArgumentException();

            var instance = new GenericInstanceType(self);
            foreach (var argument in arguments)
            {
                instance.GenericArguments.Add(argument.CopyType(md, targetType, null));
            }

            return instance;
        }

        public static TypeReference MakeGenericType1(this TypeReference self, params TypeReference[] arguments)
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
                declaringType = self.DeclaringType.MakeGenericType1(arguments);
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
                var parameterType = parameter.ParameterType.CopyType1(targetType, baseType, md, null, false);
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

        public static MethodReference GetReference1(this MethodDefinition methodDefinition, TypeDefinition currentType, ModuleDefinition md)
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
                declaringType = self.DeclaringType.MakeGenericType1(arguments);
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

        public static TypeReference CopyType1(this TypeReference self, TypeReference targetType, TypeReference sourceType, ModuleDefinition md, MethodReference targetMethod = null, bool shouldResolveGenericParaemeter = true)
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
                        return genericArgument.CopyType1(targetType, sourceType, md, targetMethod, shouldResolveGenericParaemeter);
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
                    var arguments = genericInstanceType.GenericArguments.Select(x => x.CopyType1(targetType, sourceType, md, targetMethod, shouldResolveGenericParaemeter)).ToArray();
                    return elementType.MakeGenericType1(arguments);
                }
                else if (self is ByReferenceType byReferenceType && targetType != null)
                {
                    var elementType = byReferenceType.ElementType.CopyType1(targetType, sourceType, md, targetMethod, shouldResolveGenericParaemeter);
                    return new ByReferenceType(elementType);
                }

                return md.ImportReference(self);
            }
        }

        public static TypeReference CopyType(this TypeReference self, ModuleDefinition md, TypeDefinition targetType = null, MethodDefinition targetMethod = null, bool shouldResolveGenericParaemeter = true)
        {
            if (self is GenericParameter genericParameter)
            {
                if (!shouldResolveGenericParaemeter)
                    return self;
                if (targetMethod != null && genericParameter.Type == GenericParameterType.Method)
                {
                    return targetMethod.GenericParameters.First(x => x.Name == genericParameter.Name);
                }
                else if(targetType != null)
                {
                    if (targetType.BaseType is GenericInstanceType genericInstanceType)
                    {
                        var genericArgument = genericInstanceType.GenericArguments[genericParameter.Position];
                        if (genericArgument.IsGenericParameter)
                        {
                            return genericArgument;
                        }
                        else
                        {
                            return genericArgument.CopyType(md, targetType, targetMethod);
                        }
                    }
                    else
                        return targetType.GenericParameters.FirstOrDefault(x => x.Name == genericParameter.Name) ?? self;
                }
                else
                {
                    return self;
                }
            }
            else
            {
                if(self is GenericInstanceType genericInstanceType)
                {
                    var elementType = md.ImportReference(genericInstanceType.ElementType);
                    return elementType.MakeGenericType(md, targetType, shouldResolveGenericParaemeter, genericInstanceType.GenericArguments.ToArray());
                }
                else if(self is ByReferenceType byReferenceType && targetType != null)
                {
                    var elementType = byReferenceType.ElementType.CopyType(md, targetType, targetMethod, shouldResolveGenericParaemeter);
                    return new ByReferenceType(elementType);
                }

                return md.ImportReference(self);
            }
        }
    }
}