using System;
namespace BaseAssembly
{
    public class BaseClassWithNestedGeneric
    {
        public virtual T BaseMethod<T>(Func<T> func)
        {
            return func();
        }
    }

    //public class BaseClassWithNestedGeneric1 : BaseClassWithNestedGeneric<string>
    //{
    //    public override string BaseMethod(Func<string> func)
    //    {
    //        return base.BaseMethod(func);
    //    }
    //}
}
