using System;
namespace StatementConverter.Test
{
    public partial class MethodOverloadingTestClass
    {
        public static void OverloadMethod1()
        {
        }

        public static void OverloadMethod1(Action action)
        {
            Tracker.Call("hello");
        }

        private static void OverloadMethod2(Func<string> func)
        {
        }

        private static void OverloadMethod2(Action action)
        {
            Tracker.Call("hello");
        }

        public static void OverloadMethod3(Delegate @delegate)
        {
        }

        public static void OverloadMethod3(Action action)
        {
            Tracker.Call("hello");
        }

        public static void OverloadMethod4(Action action = null)
        {
            Tracker.Call("hello");
        }

        public static void OverloadMethod4(Delegate @delegate)
        {
        }

        public static void OverloadMethod5(Action action, Func<string> func)
        {
            Tracker.Call("hello");
        }

        public static void OverloadMethod5(Func<string> a, Action b)
        {
        }

        public static void OverloadMethod6(Delegate @delegate)
        {
            Tracker.Call("hello");
        }
    }
}
