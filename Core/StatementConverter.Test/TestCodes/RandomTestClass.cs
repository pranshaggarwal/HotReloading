using System;
namespace StatementConverter.Test.TestCodes
{
    public class RandomTestClass
    {
        public static void Default()
        {
            var t = default(int);

            Tracker.Call(t);
        }

        public static void TypeOf()
        {
            var t = typeof(string);

            Tracker.Call(t);
        }

        public static void NameOf()
        {
            var hello = "hello";
            var t = nameof(hello);

            Tracker.Call(t);
        }

        public static void IsType()
        {
            var obj = "hello";
            var t = obj is string;
            Tracker.Call(t);
        }

        public static void As()
        {
            var obj = "hello";
            var t = obj as string;
            Tracker.Call(t);
        }

        public static void IsConst()
        {
            var obj = "hello";
            var t = obj is "hello";
            Tracker.Call(t);
        }
    }
}
