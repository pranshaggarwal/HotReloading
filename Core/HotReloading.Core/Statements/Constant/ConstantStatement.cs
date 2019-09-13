namespace HotReloading.Core.Statements
{
    public class ConstantStatement : Statement
    {
        public ConstantStatement(object value)
        {
            Value = value;
        }

        public object Value { get; }

        public Type Type { get; set; }
    }
}