namespace HotReloading.Core.Statements
{
    public abstract class MemberAccessStatement : Statement
    {
        public AccessModifier AccessModifier { get; set; }
    }
}