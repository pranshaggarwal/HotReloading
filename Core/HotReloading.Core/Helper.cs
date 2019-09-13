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
            return GetMethodKey(method.Name, method.Parameters.Select(x => GenerateUniqueKey(x.Type)).ToArray());
        }

        private static string GenerateUniqueKey(Type type)
        {
            if (type.IsGeneric)
                return type.Name;

            var dotNetType = ((System.Type)type);
            string key = dotNetType.ToString();
            return key;
        }
    }
}