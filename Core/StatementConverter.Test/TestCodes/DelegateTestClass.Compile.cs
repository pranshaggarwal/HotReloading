using System;
namespace StatementConverter.Test
{
    public partial class DelegateTestClass
    {
        private string TestFunction(string arg)
        {
            return arg;
        }

        private void TestFunction1(Action action)
        {
            action();
        }
    }
}
