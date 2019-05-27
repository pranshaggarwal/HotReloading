using System;
namespace HotReloading.Test.TestCodes
{
    public partial class FieldTestClass
    {
        private static string field1;

        public static void TestField1()
        {
            field1 = "hello";
            Tracker.Call(field1);
        }
    }
}
