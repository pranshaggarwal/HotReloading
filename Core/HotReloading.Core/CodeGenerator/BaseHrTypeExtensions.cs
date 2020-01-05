namespace HotReloading.Core
{
    public static class BaseHrTypeExtensions
    {
        public static string GenerateCode(this BaseHrType hrType)
        {
            var code = hrType.Name;
            if (code == "System.Void")
                code = "void";

            return code;
        }
    }
}