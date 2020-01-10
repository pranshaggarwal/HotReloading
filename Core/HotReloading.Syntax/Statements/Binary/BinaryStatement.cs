namespace HotReloading.Syntax.Statements
{
    public class BinaryStatement : IStatementCSharpSyntax
    {
        public IStatementCSharpSyntax Left { get; set; }
        public IStatementCSharpSyntax Right { get; set; }
        public BinaryOperand Operand { get; set; }
    }
}
