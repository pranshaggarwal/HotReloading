using System;
namespace StatementConverter.Test
{
    public class BaseClass
    {
        public virtual void BaseMethod()
        {
            Tracker.Call("hello");
        }

        public virtual string BaseMethod1(string str)
        {
            return str;
        }
    }
}
