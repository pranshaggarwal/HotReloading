using System;
using System.Collections.Generic;

namespace StatementConverter.Test.TestCodes
{
    public class ConditionalTestClass
    {
        public static void IfWithoutBlock()
        {
            if (true)
                Tracker.Call("hello");
        }

        public static void IfWithBlock()
        {
            if (true)
            {
                var t = "hello";
                Tracker.Call(t);
            }
        }

        public static void IfElseBlock()
        {
            if (false)
            {
                Tracker.Call("hello1");
            }
            else
            {
                Tracker.Call("hello");
            }
        }

        public static void IfElseIfBlock()
        {
            if (false)
            {
                Tracker.Call("hello1");
            }
            else if (2 == 2)
            {
                Tracker.Call("hello");
            }
        }

        public static void IfElseIfBlock1()
        {
            if (false)
            {
                Tracker.Call("hello1");
            }
            else if (2 != 2)
            {
                Tracker.Call("hello2");
            }
            else
            {
                Tracker.Call("hello");
            }
        }

        public static void IfIsPattern()
        {
            object obj = "hello";
            if (obj is string t)
            {
                Tracker.Call(t);
            }
        }

        public static void ConditionalStatement()
        {
            var t = true ? "hello" : "hello1";
            Tracker.Call(t);
        }

        public static void SwitchStatment()
        {
            switch (2)
            {
                case 1:
                    Tracker.Call("hello1");
                    break;
                case 2:
                    Tracker.Call("hello");
                    break;
            }
        }

        public static void SwitchWithMultipleCases()
        {
            switch(2)
            {
                case 1:
                case 2:
                    Tracker.Call("hello");
                    break;
                case 3:
                    Tracker.Call("hello1");
                    break;
            }
        }

        public static void SwitchWithBlock()
        {
            switch (2)
            {
                case 1:
                    Tracker.Call("hello1");
                    break;
                case 2:
                    var t = "hello";
                    Tracker.Call(t);
                    break;
            }
        }

        public static void SwitchDefault()
        {
            switch (3)
            {
                case 1:
                    Tracker.Call("hello1");
                    break;
                case 2:
                    Tracker.Call("hello2");
                    break;
                default:
                    Tracker.Call("hello");
                    break;
            }
        }

        public static void SwitchPattern()
        {
            object obj = "hello";

            switch (obj)
            {
                case string str:
                    Tracker.Call(str);
                    break;
            }
        }

        public static void SwithcPattern2()
        {
            object obj = "hello";

            switch (obj)
            {
                case string str when str == "hello":
                    Tracker.Call(str);
                    break;
            }
        }

        public static void ConditionalAccess1()
        {
            string str = "Hello";
            Tracker.Call(str?.ToLower());
        }

        public static void ConditionalAccess2()
        {
            var list = new List<string>();
            list?.Add("hello");
            var result = list?[0];
            Tracker.Call(result);
        }

        public static void ConditionalAccess3()
        {
            string str = "Hello";
            Tracker.Call(str.ToUpper().ToLower()?.ToLower());
        }

        public static void ConditionalAccess4()
        {
            var list = new List<string>
            {
                "Hello"
            };
            Tracker.Call(list?[0]?.ToUpper().ToLower()?.ToLower());
        }
    }
}
