namespace BaseAssembly
{
    public class BaseGenericClass<T>
    {
        public virtual T BaseMethod(T t)
        {
            return t;
        }
    }

    public class TEs1<T> : BaseGenericClass<T>
    {

    }
}
