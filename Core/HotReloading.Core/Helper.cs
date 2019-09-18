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

        private static string GenerateUniqueKey(BaseHrType type)
        {
            if (type is HrType hrType)
                return GenerateUniqueKey(hrType);
            return GenerateUniqueKey((GenericHrType)type);
        }

        private static string GenerateUniqueKey(HrType hrType)
        {
            return ((System.Type)hrType).ToString();
        }

        private static string GenerateUniqueKey(GenericHrType hrType)
        {
            return hrType.Name;
        }
    }
}