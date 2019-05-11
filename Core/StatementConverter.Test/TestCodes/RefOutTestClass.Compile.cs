namespace StatementConverter.Test
{
    public partial class RefOutTestClass
    {
        private void RefMethod(ref string str)
        {
            str = "hello";
        }

        private void OutMethod(out string str)
        {
            str = "hello";
        }
    }
}
