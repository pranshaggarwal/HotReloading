using System;
namespace StatementConverter.Test
{
	public partial class InvocationTestClass
	{
        private string field;

        public string Field
        {
            get => field;
        }

        public InvocationTestClass()
        {
        }


        public InvocationTestClass(string field)
        {
            this.field = field;
        }

        public static void StaticMemberMethod()
        {
            Tracker.Call();
        }

        public void InstanceMemberMethod()
        {
            Tracker.Call(field);
        }
    }
}
