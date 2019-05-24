using System;
using System.Threading.Tasks;

namespace StatementConverter.Test
{
    public partial class DelegateTestClass
    {
        public void DefineDelegate1()
        {
            var func = new Func<string, string>(TestFunction);

            Tracker.Call(func("hello"));
        }

        public void DefineDelegate2()
        {
            Func<string, string> func = TestFunction;

            Tracker.Call(func("hello"));
        }

        public void DefineDelegate3()
        {
            Func<string, string> func = delegate (string arg)
            {

                return arg;
            };

            Tracker.Call(func("hello"));
        }

        public void DefineDelegate4()
        {
            Func<string, string> func = (string arg) =>
            {
                return arg;
            };

            Tracker.Call(func("hello"));
        }

        public void DefineDelegate5()
        {
            Func<string, string> func = (arg) =>
            {
                return arg;
            };

            Tracker.Call(func("hello"));
        }

        public void DefineDelegate6()
        {
            Func<string, string> func = (arg) => arg;

            Tracker.Call(func("hello"));
        }

        public async Task DefineDelegate7()
        {
            Func<string, Task<string>> func = async (arg) =>
            {
                return arg;
            };

            Tracker.Call(await func("hello"));
        }

        public async Task DefineDelegate8()
        {
            Func<string, Task<string>> func = TestFunction2;

            Tracker.Call(await func("hello"));
        }

        public void DefineDelegate9()
        {
            Action<object> action = Tracker.Call;

            action("hello");
        }

        public void DefineDelegate10()
        {
            var func = new Func<string,string>((str) => TestFunction(str));

            Tracker.Call(func("hello"));
        }

        public void AddDelegate()
        {
            int counter = 0;
            Action action = () => counter++;

            action += () => counter++;

            action();

            Tracker.Call(counter);
        }

        public void RemoveDelegate()
        {
            Tracker.Call("hello");
            Action<object> action = (obj) => { };
            action += Tracker.Call;

            action -= Tracker.Call;

            action("hello1");
        }

        public void PassDelegateToMethod1()
        {
            Action action = () => Tracker.Call("hello");

            TestFunction1(action);
        }

        public void PassDelegateToMethod2()
        {
            TestFunction1(() => Tracker.Call("hello"));
        }

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
