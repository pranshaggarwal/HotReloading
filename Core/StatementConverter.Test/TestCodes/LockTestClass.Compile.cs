namespace StatementConverter.Test
{
    public partial class LockTestClass
    {
        private static readonly object _lock = new object();
        private static int sharedResource = 0;
    }
}
