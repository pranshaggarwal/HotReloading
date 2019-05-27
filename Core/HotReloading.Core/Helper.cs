using System;
using System.Linq;

namespace HotReloading.Core
{
    public class Helper
    {
        public static string GetMethodKey(string methodName, params string[] parameterTypes)
        {
            string key = methodName;

            foreach (var parameter in parameterTypes)
            {
                key += $"`{(parameter)}";
            }

            return key;
        }

        public static string GetMethodKey(Method method)
        {
            return GetMethodKey(method.Name, method.Parameters.Select(x => x.Type.Key).ToArray());
        }

        public static string GetFieldKey(Field field)
        {
            return GetFieldKey(field.Name);
        }

        public static string GetFieldKey(string fieldName)
        {
            return fieldName;
        }

        public static string GetPropertyKey(Property property)
        {
            return GetPropertyKey(property.Name);
        }

        public static string GetPropertyKey(string propertyName)
        {
            return propertyName;
        }
    }
}