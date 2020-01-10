namespace HotReloading.Syntax.Statements
{
    public class UnaryStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Operand { get; set; }
        public UnaryOperand Operator { get; set; }
    }
}
