namespace HotReloading.Syntax.Statements
{
    public abstract class MemberAccessStatement : IStatementCSharpSyntax
    {
        public AccessModifier AccessModifier { get; set; }
    }
}