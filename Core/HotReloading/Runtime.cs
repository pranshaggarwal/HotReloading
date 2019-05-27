using System;
using System.Collections.Generic;
using System.Linq;
using HotReloading.Core;

namespace HotReloading
{
    public static class Runtime
    {
        public static void HandleCodeChange(CodeChange codeChange)
        {
            foreach (var field in codeChange.Fields)
                HandleFieldChange(field);

            foreach (var property in codeChange.Properties)
                HandlePropertyChange(property);

            foreach (var method in codeChange.Methods)
            {
                HandleMethodChange(method);
            }

            foreach(var container in RuntimeMemory.Fields.SelectMany(x => x.Value)
                .Where(x => codeChange.Fields.Any(y => y == x.Field)))
            {
                UpdateInstanceField(container);
            }

            foreach(var container in RuntimeMemory.Methods.SelectMany(x => 
                                                                x.Value).Where(x => 
                                                codeChange.Methods.Any(y => y == x.Method)))
            {
                UpdateInstanceMethod(container);
            }
        }

        private static void HandleFieldChange(Field field)
        {
            if (!RuntimeMemory.Fields.ContainsKey(field.ParentType))
            {
                RuntimeMemory.Fields.Add(field.ParentType, new List<FieldContainer>());
            }

            var fields = RuntimeMemory.Fields[field.ParentType];

            var fieldKey = Helper.GetFieldKey(field);

            var existingField = fields.SingleOrDefault(x => Helper.GetFieldKey(x.Field) == fieldKey);

            if (existingField != null)
            {
                fields.Remove(existingField);
            }

            fields.Add(new FieldContainer
            {
                Field =field
            });
        }

        private static void HandlePropertyChange(Property property)
        {
            if (!RuntimeMemory.Properties.ContainsKey(property.ParentType))
            {
                RuntimeMemory.Properties.Add(property.ParentType, new List<IPropertyContainer>());
            }

            var properties = RuntimeMemory.Properties[property.ParentType];

            var propertyKey = Helper.GetPropertyKey(property);

            var existingProperty = properties.SingleOrDefault(x => Helper.GetPropertyKey(x.Property) == propertyKey);

            if (existingProperty != null)
            {
                properties.Remove(existingProperty);
            }

            properties.Add(new PropertyContainer(property));
        }

        private static void HandleMethodChange(Method method)
        {
            if (!RuntimeMemory.Methods.ContainsKey(method.ParentType))
            {
                RuntimeMemory.Methods.Add(method.ParentType, new List<IMethodContainer>());
            }

            var methods = RuntimeMemory.Methods[method.ParentType];

            var methodKey = Helper.GetMethodKey(method);

            var existingMethod = methods.SingleOrDefault(x => Helper.GetMethodKey(x.Method) == methodKey);

            if (existingMethod != null)
            {
                methods.Remove(existingMethod);
            }

            MethodContainer container = new MethodContainer(method);
            methods.Add(container);
        }

        private static void UpdateInstanceMethod(IMethodContainer container)
        {
            var methodKey = Helper.GetMethodKey(container.Method);
            if (!container.Method.IsStatic)
            {
                foreach (var instance in RuntimeMemory.MemoryInstances.Where(x => x.GetType() == container.Method.ParentType))
                {
                    if (instance.InstanceMethods.ContainsKey(methodKey))
                        instance.InstanceMethods[methodKey] = container.GetDelegate();
                    else
                        instance.InstanceMethods.Add(methodKey, container.GetDelegate());
                }
            }
        }

        private static void UpdateInstanceField(FieldContainer container)
        {
            var fieldKey = Helper.GetFieldKey(container.Field);
            if(!container.Field.IsStatic)
            {
                foreach(var instance in RuntimeMemory.MemoryInstances.Where(x => x.GetType() == container.Field.ParentType))
                {
                    var field = new FieldContainer
                    {
                        Field = container.Field
                    };
                    if (instance.InstanceFields.ContainsKey(fieldKey))
                        instance.InstanceFields[fieldKey] = field;
                    else
                        instance.InstanceFields.Add(fieldKey, field);
                }
            }
        }

        private static void UpdateInstanceProperty(IPropertyContainer container)
        {
            var propertyKey = Helper.GetPropertyKey(container.Property);
            if (!container.Property.IsStatic)
            {
                foreach (var instance in RuntimeMemory.MemoryInstances.Where(x => x.GetType() == container.Property.ParentType))
                {
                    if (instance.InstanceProperties.ContainsKey(propertyKey))
                        instance.InstanceProperties[propertyKey] = new PropertyContainer(container.Property);
                    else
                        instance.InstanceProperties.Add(propertyKey, new PropertyContainer(container.Property));
                }
            }
        }

        public static Delegate GetMethodDelegate(Type parentType, string key)
        {
            if(RuntimeMemory.Methods.ContainsKey(parentType))
                return RuntimeMemory.Methods[parentType].SingleOrDefault(x => Helper.GetMethodKey(x.Method) == key)?.GetDelegate();
            return null;
        }

        public static Dictionary<string, Delegate> GetInitialInstanceMethods(IInstanceClass instanceClass)
        {
            if(!RuntimeMemory.MemoryInstances.Contains(instanceClass))
                RuntimeMemory.MemoryInstances.Add(instanceClass);
            var type = instanceClass.GetType();
            var instanceMethods = new Dictionary<string, Delegate>();

            if (RuntimeMemory.Methods.ContainsKey(type))
                foreach (var instanceMethod in RuntimeMemory.Methods[type].Where(x => !x.Method.IsStatic))
                    instanceMethods.Add(Helper.GetMethodKey(instanceMethod.Method), instanceMethod.GetDelegate());

            return instanceMethods;
        }

        public static Dictionary<string, FieldContainer> GetInitialInstanceFields(IInstanceClass instanceClass)
        {
            if (!RuntimeMemory.MemoryInstances.Contains(instanceClass))
                RuntimeMemory.MemoryInstances.Add(instanceClass);
            var type = instanceClass.GetType();
            var instanceFields = new Dictionary<string, FieldContainer>();

            if (RuntimeMemory.Fields.ContainsKey(type))
                foreach (var instanceField in RuntimeMemory.Fields[type].Where(x => !x.Field.IsStatic))
                    instanceFields.Add(Helper.GetFieldKey(instanceField.Field), new FieldContainer() { Field = instanceField.Field });

            return instanceFields;
        }

        public static Dictionary<string, IPropertyContainer> GetInitialProperties(IInstanceClass instanceClass)
        {
            if (!RuntimeMemory.MemoryInstances.Contains(instanceClass))
                RuntimeMemory.MemoryInstances.Add(instanceClass);
            var type = instanceClass.GetType();
            var instanceProperties = new Dictionary<string, IPropertyContainer>();

            if (RuntimeMemory.Properties.ContainsKey(type))
                foreach (var instanceProperty in RuntimeMemory.Properties[type].Where(x => !x.Property.IsStatic))
                    instanceProperties.Add(Helper.GetPropertyKey(instanceProperty.Property), new PropertyContainer(instanceProperty.Property));

            return instanceProperties;
        }
    }
}