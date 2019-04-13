using System;
namespace StatementConverter.Test
{
    public class LoopTestClass
    {
        public static void TestWhile()
        {
            int i = 0;
            while(i < 5)
            {
                i = i + 1;
            }

            Tracker.Call(i);
        }

        public static void TestDoWhile()
        {
            int i = 0;
            do
            {
                i = i + 1;
            } while (i < 5);

            Tracker.Call(i);
        }

        public static void TestFor1()
        {
            string test = "hello";
            string result = "";
            for(int i = 0; i < test.Length; i++)
            {
                result += test[i];
            }

            Tracker.Call(result);
        }

        public static void TestFor2()
        {
            string test = "hello";
            string result = "";
            for (int i = 0; ; i++)
            {
                if (i >= 5)
                    break;
                result += test[i];
            }

            Tracker.Call(result);
        }

        public static void TestFor3()
        {
            string test = "hello";
            string result = "";
            int i = 0;
            for (; ; i++)
            {
                if (i >= 5)
                    break;
                result += test[i];
            }

            Tracker.Call(result);
        }

        public static void TestFor4()
        {
            string test = "hello";
            string result = "";
            int i = 0;
            for (; ; )
            {
                if (i >= 5)
                    break;
                result += test[i];
                i++;
            }

            Tracker.Call(result);
        }

        public static void TestFor5()
        {
            string test = "hello";
            string result = "";
            int i;
            for (i = 0; i < test.Length; i++)
            {
                result += test[i];
            }

            Tracker.Call(result);
        }

        public static void TestFor6()
        {
            string test = "thello";
            string result = "";
            int i;
            for (i = 0; i < test.Length; i++)
            {
                if (test[i] == 't')
                    continue;
                result += test[i];
            }

            Tracker.Call(result);
        }

        public static void TestForEach()
        {
            var test = "hello";
            var result = "";
            foreach(var c in test)
            {
                result += c;
            }

            Tracker.Call(result);
        }
    }
}
