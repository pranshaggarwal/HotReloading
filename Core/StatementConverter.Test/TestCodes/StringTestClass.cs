namespace StatementConverter.Test
{
    public class StringTestClass
    {
        public static void StringInterpolation()
        {
            Tracker.Call($"{1}hello{1}");
        }
    }
}
