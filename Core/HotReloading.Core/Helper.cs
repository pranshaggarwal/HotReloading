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
    }
}