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
                attributes |= MethodAttributes.NewSlot | MethodAttributes.Virtual;

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
    }
}