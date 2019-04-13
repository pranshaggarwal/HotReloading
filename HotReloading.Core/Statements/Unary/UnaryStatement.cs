using System;
namespace HotReloading.Core.Statements
{
    public class UnaryStatement : Statement
    {
        public Statement Operand { get; set; }
        public UnaryOperand Operator { get; set; }
    }
}
