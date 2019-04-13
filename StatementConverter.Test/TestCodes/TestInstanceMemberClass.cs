namespace StatementConverter.Test
{
    public class TestInstanceMemberClass
    {
        public static string StaticProperty
        {
            set
            {
                Tracker.LastValue = value;
            }
        }

        public string Property
        {
            set
            {
                Tracker.LastValue = value;
            }
        }

        public ObjectTestClass Object = new ObjectTestClass();

        public void Test()
        {
            Tracker.Call();
        }
    }
}

