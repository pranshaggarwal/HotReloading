namespace StatementConverter.Test
{
    public class InstanceTestClass
    {
        public string Property { get; set; }

        public string Property2 { get; set; }

        public InstanceTestClass()
        {
            Tracker.Call();
        }

        public InstanceTestClass(string p)
        {
            Tracker.Call(p);
        }
    }
}

