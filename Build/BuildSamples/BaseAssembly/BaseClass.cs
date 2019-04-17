using System;
namespace BaseAssembly
{
    public class BaseClass
    {
        public virtual void BaseMethod()
        {
            Tracker.Call("default");
        }

        public virtual string BaseMethod1(string str)
        {
            return str;
        }
    }
}
