using System.Threading.Tasks;

namespace StatementConverter.Test
{
	public partial class AsyncAwaitTestClass
    {
        public async Task AsyncMethodWithNoReturn()
        {
            await Task.Delay(1);
            Tracker.Call("hello");
        }

        public async Task<string> AsyncMethodWithStringReturn()
        {
            await Task.Delay(1);
            return "hello";
        }
    }
}
