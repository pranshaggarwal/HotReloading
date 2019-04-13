using System;
namespace StatementConverter.Test
{
    public class ObjectCreationTestClass
    {
        public static void CreateNewObject()
        {
            InstanceTestClass testClass = new InstanceTestClass();
        }

        public static void CreateNewObjectWithParameter()
        {
            InstanceTestClass testClass = new InstanceTestClass("hello");
        }

        public static void CreateObjectWithInitializer()
        {
            InstanceTestClass testClass = new InstanceTestClass()
            {
                Property = "Hello"
            };


            Tracker.Call(testClass.Property);
        }

        public static void CreateObjectWithInitializer1()
        {
            InstanceTestClass testClass = new InstanceTestClass()
            {
                Property = "Hel",
                Property2 = "lo"
            };


            Tracker.Call(testClass.Property + testClass.Property2);
        }

        public static void CreateObjectWithInitializerWithoutParanthesis()
        {
            InstanceTestClass testClass = new InstanceTestClass
            {
                Property = "Hello"
            };

            Tracker.Call(testClass.Property);
        }

        public static void PassNewObjectToMethod()
        {
            Tracker.CallWithInstanceTestClass(new InstanceTestClass
            {
                Property = "Hello"
            });
        }
    }
}
