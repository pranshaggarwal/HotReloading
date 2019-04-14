using System;
namespace StatementConverter.Test
{
    public partial class AssignmentTestClass
    {
        public string field;
        public string Property { get; set; }

        public static void AssignLocalVariable()
        {
            string test;
            test = "hello";

            Tracker.Call(test);
        }

        public static void AssignLocalVariableFromOtherLocalVariable()
        {
            string test;
            test = "hello";
            string test2;
            test2 = test;

            Tracker.Call(test2);
        }

        public static void AssignParameterToLocalVariable(string test)
        {
            string test2;
            test2 = test;

            Tracker.Call(test2);
        }

        public void AssignField()
        {
            field = "hello";

            Tracker.Call(field);
        }

        public void AssignProperty()
        {
            Property = "hello";

            Tracker.Call(Property);
        }

        public static void AssignInstancePropertyOfOtherClass()
        {
            var instance = new TestInstanceMemberClass();

            instance.Property = "hello";
        }

        public static void AssignStaticPropertyOfOtherClass()
        {
            TestInstanceMemberClass.StaticProperty = "hello";
        }
    }
}
