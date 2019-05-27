using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExpressions.Microsoft.CSharp;

namespace HotReloading.Core
{
    public class RuntimeMemory
    {
        public static List<IInstanceClass> MemoryInstances = new List<IInstanceClass>();
        public static Dictionary<Type, List<IMethodContainer>> Methods { get; private set; } = new Dictionary<Type, List<IMethodContainer>>();
        public static Dictionary<Type, List<FieldContainer>> Fields { get; private set; } = new Dictionary<Type, List<FieldContainer>>();
        public static Dictionary<Type, List<IPropertyContainer>> Properties { get; private set; } = new Dictionary<Type, List<IPropertyContainer>>();

        public static CSharpLamdaExpression GetMethod(Type @class, string key)
        {
            if (RuntimeMemory.Methods.ContainsKey(@class))
            {
                var method = RuntimeMemory.Methods[@class].SingleOrDefault(x => Helper.GetMethodKey(x.Method) == key);
                if (method != null)
                    return method.GetExpression();
            }

            return null;
        }

        public static FieldContainer GetField(Type @class, string key)
        {
            if (RuntimeMemory.Fields.ContainsKey(@class))
            {
                var field = RuntimeMemory.Fields[@class].SingleOrDefault(x => Helper.GetFieldKey(x.Field) == key);
                return field;
            }

            return null;
        }

        public static IPropertyContainer GetProperty(Type @class, string key)
        {
            if (RuntimeMemory.Properties.ContainsKey(@class))
            {
                var propertyContainer = RuntimeMemory.Properties[@class].SingleOrDefault(x => Helper.GetPropertyKey(x.Property) == key);
                return propertyContainer;
            }

            return null;
        }

        public static void Reset()
        {
            MemoryInstances.Clear();
            Methods.Clear();
            Fields.Clear();
            Properties.Clear();
        }
    }
}