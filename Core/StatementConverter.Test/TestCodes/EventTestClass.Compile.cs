using System;

namespace StatementConverter.Test
{
    public partial class EventTestClass
    {
        public event Action TestEvent;

        public static event Action StaticEvent;

        private void EventHandler()
        {
            Tracker.Call("hello");
        }
    }
}
