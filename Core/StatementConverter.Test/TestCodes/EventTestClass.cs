using System;

namespace StatementConverter.Test
{
    public partial class EventTestClass
    {
        public event Action TestEvent;

        public static event Action StaticEvent;

        public void AddEventHandler1()
        {
            var instance = new ClassWithEvent();
            instance.TestEvent += () => Tracker.Call("hello");

            instance.InvokeEvent();
        }

        public void AddEventHandler2()
        {
            var instance = new ClassWithEvent();
            instance.TestEvent += delegate
            {
                Tracker.Call("hello");
            };

            instance.InvokeEvent();
        }

        public void AddEventHandler3()
        {
            var instance = new ClassWithEvent();
            instance.TestEvent += EventHandler;

            instance.InvokeEvent();
        }

        public void AddEventHandler4()
        {
            ClassWithEvent.StaticEvent += EventHandler;

            ClassWithEvent.InvokeStaticEvent();
        }

        public void RemoveEventHandler4()
        {
            var instance = new ClassWithEvent();
            Tracker.Call("hello1");
            instance.TestEvent += EventHandler;
            instance.TestEvent -= EventHandler;
            instance.InvokeEvent();
        }

        public void TestEventInvoke()
        {
            TestEvent += EventHandler;
            TestEvent.Invoke();
        }

        public void TestStaticEventInvoke()
        {
            StaticEvent += EventHandler;
            StaticEvent.Invoke();
        }

        private void EventHandler()
        {
            Tracker.Call("hello");
        }
    }
}
