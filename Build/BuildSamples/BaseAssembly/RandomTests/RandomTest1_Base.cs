using System;
namespace BaseAssembly.RandomTests
{
    public class RandomTest1_Base<T> : RandomTest1_Base_Base
    {
        public virtual T TestMethod1(T t)
        {
            return default(T);
        }
    }

    public class RandomTest1_Base_Base
    {
        public virtual T TestMethod<T>(T t)
        {
            return default(T);
        }
    }

    public class RandomTest2_Base : RandomTest2_Base_Base
    {
        public override K TestMethod<K>(K t)
        {
            return base.TestMethod(t);
        }

        public virtual T TestMethod1<T>(T t)
        {
            return default(T);
        }
    }

    public class RandomTest2_Base_Base
    {
        public virtual T TestMethod<T>(T t)
        {
            return default(T);
        }
    }

    public class RandomTest3_Base : RandomTest3_Base_Base
    {
        public virtual T TestMethod1<T>(Func<T> t)
        {
            return t();
        }
    }

    public class RandomTest3_Base_Base
    {
        public virtual T TestMethod<T>(Func<T> t)
        {
            return t();
        }
    }

    public class RandomTest4_Base : RandomTest4_Base_Base
    {
        public virtual T TestMethod1<T>(Func<T> t)
        {
            return t();
        }

        public override T TestMethod<T>(Func<T> t)
        {
            return base.TestMethod(t);
        }
    }

    public class RandomTest4_Base_Base
    {
        public virtual T TestMethod<T>(Func<T> t)
        {
            return t();
        }
    }

    public class RandomTest5_Base<T, K> : RandomTest5_Base_Base<K>
    {
        public override K TestMethod(Func<K> t)
        {
            return base.TestMethod(t);
        }

        public virtual T TestMethod1(T p1, Func<K> p2)
        {
            return p1;
        }
    }

    public class RandomTest5_Base_Base<T>
    {
        public virtual T TestMethod(Func<T> t)
        {
            return t();
        }
    }

    public class RandomTest6_Base<T, K> : RandomTest5_Base_Base<K>
    {
        public virtual T TestMethod1(T p1, Func<K> p2)
        {
            return p1;
        }
    }

    public class RandomTest6_Base_Base<T>
    {
        public virtual T TestMethod(Func<T> t)
        {
            return t();
        }
    }

    public class RandomTest7_Base<T> : RandomTest7_Base_Base<string>
    {
        public override string TestMethod(Func<string> t)
        {
            return base.TestMethod(t);
        }

        public virtual T TestMethod1(T p1, Func<string> p2)
        {
            return p1;
        }
    }

    public class RandomTest7_Base_Base<T>
    {
        public virtual T TestMethod(Func<T> t)
        {
            return t();
        }
    }

    public class RandomTest8_Base<T>
    {
        public virtual T TestMethod1(Func<T> p2)
        {
            return p2();
        }
    }
}
