
namespace StatementConverter.Test.TestCodes
{
    public class UnaryTestClass
    {
        public static void NegativeNumber()
        {
            int t = 5;
            Tracker.Call(-t);
        }

        public static void PositiveNumber()
        {
            int t = 5;
            Tracker.Call(+5);
        }

        public static void Decrement()
        {
            int t = 5;
            t--;
            Tracker.Call(t);
        }

        public static void Increment()
        {
            int t = 5;
            t++;
            Tracker.Call(t);
        }

        public static void PostDecrement()
        {
            var t = 5;
            Tracker.Call(t--);
        }

        public static void PostIncrement()
        {
            var t = 5;
            Tracker.Call(t++);
        }

        public static void PreDecrement()
        {
            var t = 5;
            Tracker.Call(--t);
        }

        public static void PreIncrement()
        {
            var t = 5;
            Tracker.Call(++t);
        }

        public static void Convert()
        {
            object t = 5;
            int x = (int)5;
            Tracker.Call(x);
        }

        public static void Not()
        {
            var t = !false;
            Tracker.Call(t);
        }

        public static void OnesComplement()
        {
            var t = 0b00110011;
            Tracker.Call(~t);
        }
    }
}
