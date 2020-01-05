using HotReloading.Syntax;

namespace HotReloading.Core
{
    public static class AccessModifierExtensions
    {
        public static string GenerateCode(this AccessModifier accessModifier)
        {
            switch (accessModifier)
            {
                case AccessModifier.ProtectedInternal:
                    return "protected internal";
                default:
                    return accessModifier.ToString().ToLower();
            }
        }
    }
}