using System;
namespace StatementConverter.Test
{
	public partial class InvocationTestClass
	{
        private string field;

        private string Field
        {
            get => field;
        }

        public InvocationTestClass()
        {
        }

        public void OptionalParameterMethod1(string str = null)
        {
            if(str == null)
            {
                Tracker.Call("hello");
            }
        }

        public void OptionalParameterMethod2(string str = "default")
        {
            Tracker.Call("default");
        }

        public void OptionalNamedParameterMethod(string str = "", int i = 0)
        {
            Tracker.Call(str + i);
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
