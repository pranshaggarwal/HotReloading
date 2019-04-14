using System;
using System.Collections.Generic;

namespace StatementConverter.Test.TestCodes
{
    public class ListDictionaryTestClass
    {
        public static void InitList()
        {
            var list = new List<int>();
            list.Add(2);

            Tracker.Call(list[0]);
        }

        public static void InitDictionary()
        {
            var dic = new Dictionary<int, string>();
            dic.Add(2, "hello");

            Tracker.Call(dic[2]);
        }

        public static void InitListWithListInitializer()
        {
            var list = new List<int>()
            {
                1, 2, 3
            };

            Tracker.Call(list[1]);
        }

        public static void InitDicWithDicInitializer()
        {
            var dic = new Dictionary<int, string>()
            {
                { 1, "hello" }, {2, "hello1"}
            };

            Tracker.Call(dic[1]);
        }

        public static void InitListWithListInitializerWithoutParanthesis()
        {
            var list = new List<int>
            {
                1, 2, 3
            };

            Tracker.Call(list[1]);
        }

        public static void InitDicWithDicInitializerWithoutParanthesis()
        {
            var dic = new Dictionary<int, string>
            {
                { 1, "hello" }, {2, "hello1"}
            };

            Tracker.Call(dic[1]);
        }
    }
}
