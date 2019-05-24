using System;
using System.Linq;
using System.Text.RegularExpressions;
using Mono.Cecil;

namespace ILDynaRec
{
    public static class CecilExtensions
    {
        const string FieldPrefix = "_\u273F_";
        const string FieldSuffix = "_field";

        public static string GetHotpatchFieldName(this MethodReference method) {
            return FieldPrefix + method.GetNormalizedFullName() + FieldSuffix;
        }

        public static bool IsHotpatchField(this FieldDefinition field) {
            return field.Name.StartsWith(FieldPrefix) && field.Name.EndsWith(FieldSuffix);
        }

        static Regex regexClear = new Regex(@"[\(\)\[\]]");
        static Regex regexUnderscore = new Regex(@"[\. :<>]");
        public static string GetNormalizedFullName(this MethodReference method) {
            var str = method.FullName.Replace("::", "_");
            str = regexClear.Replace(str, "");
            str = regexUnderscore.Replace(str, "_");
            str = str.Replace("&", "_amp");

            return str;
        }
    }
}
