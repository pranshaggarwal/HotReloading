using System;
namespace StatementConverter.Test
{
    public class ClassWithEvent
    {
        public event Action TestEvent;

        public void InvokeEvent()
        {
            TestEvent?.Invoke();
        }
    }
}
