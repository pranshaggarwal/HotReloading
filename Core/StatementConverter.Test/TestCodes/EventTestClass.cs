namespace StatementConverter.Test
{
    public partial class EventTestClass
    {
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

        public void RemoveEventHandler4()
        {
            var instance = new ClassWithEvent();

            instance.TestEvent += EventHandler;
            instance.TestEvent -= EventHandler;
            instance.InvokeEvent();
        }
    }
}
