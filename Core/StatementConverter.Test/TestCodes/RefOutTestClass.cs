namespace StatementConverter.Test
{
    public partial class RefOutTestClass
    {
        public void PassRefParameterToMethod()
        {
            string value = "";
            RefMethod(ref value);

            Tracker.Call(value);
        }

        public void PassOutParameterToMethod()
        {
            string value;
            OutMethod(out value);

            Tracker.Call(value);
        }

        public void DefineRefMethod(ref string str)
        {
            str = "hello";
        }

        public void DefineOutMethod(out string str)
        {
            str = "hello";
        }
    }
}
