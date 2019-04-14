using System;
namespace StatementConverter.Test
{
    public class TestDisposable : IDisposable
    {
        public void Dispose()
        {
            Tracker.Call("hello");
        }
    }
}
