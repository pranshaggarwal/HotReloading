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
}
