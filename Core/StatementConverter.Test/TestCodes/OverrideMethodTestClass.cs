using System;
namespace StatementConverter.Test
{
    public partial class OverrideMethodTestClass : BaseClass
    {
        public override void BaseMethod()
        {
            base.BaseMethod();
        }

        public override string BaseMethod1(string str)
        {
            return base.BaseMethod1(str);
        }
    }
}
