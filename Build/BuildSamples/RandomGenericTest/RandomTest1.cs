using System;
using BaseAssembly.RandomTests;

namespace RandomGenericTest
{
    public class RandomTest1 : RandomTest1_Base<string>
    {
    }

    public class RandomTest1_1<T> : RandomTest1_Base<T>
    {

    }

    public class RandomTest1_2<T> : RandomTest1_Base<T>
    {
        public override T TestMethod1(T t)
        {
            return base.TestMethod(t);
        }
    }

    public class RandomTest2 : RandomTest2_Base
    {

    }

    public class RandomTest3 : RandomTest3_Base
    {

    }

    public class RandomTest4 : RandomTest4_Base
    {

    }

    public class RandomTest5<A, B> : RandomTest5_Base<A, B>
    {

    }

    public class RandomTest6<A, B> : RandomTest6_Base<A, B>
    {

    }

    public class RandomTest7<A> : RandomTest7_Base<A>
    {

    }

    public class RandomTest8<T, K> : RandomTest8_Base<K>
    {
        public T TestMethod1(T p1, Func<K> func)
        {
            return p1;
        }
    }

    public class RandomTest8_1<T, K> : RandomTest8_Base<Func<K>>
    {
        public T TestMethod1(T p1, Func<K> func)
        {
            return p1;
        }
    }
}
