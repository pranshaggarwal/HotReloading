using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.Core.Statements
{
    public class ConstantStatement : IStatementCSharpSyntax
    {
        public ConstantStatement(object value)
        {
            Value = value;
        }

        public object Value { get; }

        public BaseHrType Type { get; set; }
    }
}