namespace HotReloading.Syntax
{
    public class Field : ICSharpSyntax
    {
        public AccessModifier AccessModifier { get; set; }
        public BaseHrType Type { get; set; }
    }
}