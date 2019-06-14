namespace StatementConverter.Test
{
    public partial class LockTestClass
    {
        private static readonly object _lock = new object();
        private static int sharedResource = 0;

        public static void Test()
        {
            lock (_lock)
            {
                for (int i = 0; i < 1000000; i++)
                {
                    sharedResource++;
                }
            }
        }
    }
}
