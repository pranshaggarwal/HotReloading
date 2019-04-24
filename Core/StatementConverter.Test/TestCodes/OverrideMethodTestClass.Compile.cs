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

        private void HotReloadingBase_BaseMethod()
        {
            base.BaseMethod();
        }

        private string HotReloadingBase_BaseMethod1(string p)
        {
            return base.BaseMethod1(p);
        }
    }
}
