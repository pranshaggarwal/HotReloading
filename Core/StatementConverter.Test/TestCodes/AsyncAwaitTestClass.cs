using System.Threading.Tasks;

namespace StatementConverter.Test
{
    public partial class AsyncAwaitTestClass
    {
        public async void TestAwaitTaskCall()
        {
            await AsyncMethodWithNoReturn();
        }

        public async void TestAwaitTaskStringCall()
        {
            var test = await AsyncMethodWithStringReturn();

            Tracker.Call(test);
        }

        public async Task<string> TestAwaitReturnStringCall()
        {
            var test = await AsyncMethodWithStringReturn();

            return test;
        }

        public async Task AsyncMethodWithNoReturn()
        {
            await Task.Delay(1);
            System.Diagnostics.Debug.WriteLine("hello");
        }

        public async Task<string> AsyncMethodWithStringReturn()
        {
            await Task.Delay(1);
            return "hello";
        }
    }
}
