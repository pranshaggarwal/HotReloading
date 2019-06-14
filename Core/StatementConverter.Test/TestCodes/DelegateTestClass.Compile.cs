using System;
using System.Threading.Tasks;

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

        private async Task<string> TestFunction2(string arg)
        {
            return arg;
        }
    }
}
