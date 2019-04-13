using System;
namespace HotReloading.Core.Statements
{
    public class BinaryStatement : Statement
    {
        public Statement Left { get; set; }
        public Statement Right { get; set; }
        public BinaryOperand Operand { get; set; }
    }
}
