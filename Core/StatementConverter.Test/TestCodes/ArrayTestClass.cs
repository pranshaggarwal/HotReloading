using System;
namespace StatementConverter.Test.TestCodes
{
    public partial class ArrayTestClass
    {
        public static void InitOneDArrayWithoutContent()
        {
            int[] arr = new int[5];

            Tracker.Call(arr.Length);
        }

        public static void InitTwoDArrayWithoutContent()
        {
            int[,] arr = new int[5, 3];
            Tracker.Call(arr.Length);
        }

        public static void InitOneDArrayWithContent()
        {
            int[] arr = new int[] { 1, 2, 3 };

            Tracker.Call(arr.Length);
        }

        public static void InitTwoDArrayWithContent()
        {
            int[,] arr = new int[,] { { 1, 2, 3 }, { 1, 2, 3 } };

            Tracker.Call(arr.Length);
        }

        public static void InitOneDArrayWithArrayInitializer()
        {
            int[] arr = { 1, 2, 3 };

            Tracker.Call(arr.Length);
        }

        public static void InitTwoDArrayWithArrayInitializer()
        {
            int[,] arr = { { 1, 2, 3 }, { 1, 2, 3 } };

            Tracker.Call(arr.Length);
        }

        public static void GetOneDArrayValue()
        {
            int[] arr = { 1, 2, 3 };

            Tracker.Call(arr[1]);
        }

        public static void GetTwoDArrayValue()
        {
            int[,] arr = { { 1, 2, 3 }, { 4, 5, 6 } };

            Tracker.Call(arr[1, 1]);
        }

        public static void SetOneDArrayValue()
        {
            int[] arr = new int[5];
            arr[0] = 1;
            Tracker.Call(arr[0]);
        }

        public static void SetTwoDArrayValue()
        {
            int[,] arr = new int[5, 3];
            arr[0, 0] = 2;
            Tracker.Call(arr[0, 0]);
        }
    }
}
