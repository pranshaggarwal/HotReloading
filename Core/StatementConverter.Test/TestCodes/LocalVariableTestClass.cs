using System;
namespace StatementConverter.Test
{
    public class LocalVariableTestClass
    {
        public static void DeclareSingleLocalVariable()
        {
            bool test;

            Tracker.Call();
        }

        public static void DeclareMultipleLocalVariable()
        {
            bool test1, test2, test3;

            Tracker.Call();
        }

        public static void DeclareAndAssignLocalVariable()
        {
            string test = "hello";

            Tracker.Call(test);
        }

        public static void AssignParameterToLocalVariableWithDeclaration(string test)
        {
            string test2 = test;

            Tracker.Call(test2);
        }
    }
}
