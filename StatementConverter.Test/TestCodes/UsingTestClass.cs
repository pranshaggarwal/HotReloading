namespace StatementConverter.Test
{
    public class UsingTestClass
    {
        public static void TestUsing1()
        {
            using (new TestDisposable())
            {
                System.Diagnostics.Debug.WriteLine("hello");
            }
        }

        public static void TestUsing2()
        {
            using (var test = new TestDisposable())
            {
                System.Diagnostics.Debug.WriteLine("hello");
            }
        }

        public static void TestUsing3()
        {
            var test = new TestDisposable();
            using (test)
            {
                System.Diagnostics.Debug.WriteLine("hello");
            }
        }
    }
}
