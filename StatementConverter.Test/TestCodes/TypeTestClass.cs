using System;
namespace StatementConverter.Test
{
    public static class TypeTestClass
    {
        public static void SimpleType()
        {
            InstanceTestClass t;
            var t1 = t;
        }

        public static void GenericTypeWithOneArgument()
        {
            GenericClass<int> t;
            var t1 = t;
        }

        public static void GenericTypeWithTwoArgument()
        {
            GenericClass<int,int> t;
            var t1 = t;
        }

        public static void OneDArray()
        {
            int[] t;
            var t1 = t;
        }

        public static void TwoDArray()
        {
            int[][] t;
            var t1 = t;
        }
    }
}
