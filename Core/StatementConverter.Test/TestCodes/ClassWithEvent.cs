using System;
namespace StatementConverter.Test
{
    public class ClassWithEvent
    {
        public event Action TestEvent;
        public static event Action StaticEvent;

        public void InvokeEvent()
        {
            TestEvent?.Invoke();
        }

        public static void InvokeStaticEvent()
        {
            StaticEvent?.Invoke();
        }
    }
}
