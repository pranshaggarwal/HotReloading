namespace BaseAssembly
{
    public class BaseClassWithGenericMethod
    {
        public virtual T BaseMethod<T>(T t)
        {
            return t;
        }
    }
}
