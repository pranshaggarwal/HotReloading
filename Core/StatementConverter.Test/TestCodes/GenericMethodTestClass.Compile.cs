using System;
namespace StatementConverter.Test
{
    public partial class GenericMethodTestClass
    {
        public T GenericMethod<T>(T t)
        {
            return default(T);
        }
    }
}
