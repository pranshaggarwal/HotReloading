using System;

namespace StatementConverter.Test
{
    public static class Tracker
    {
        public static bool MethodCalled;

        public static object LastValue;

        public static void Call()
        {
            MethodCalled = true;
        }

        public static void Reset()
        {
            MethodCalled = false;
            LastValue = null;
        }

        public static void Call(object value)
        {
            LastValue = value;
        }

        public static void CallWithInstanceTestClass(InstanceTestClass instanceTestClass)
        {
            LastValue = instanceTestClass.Property;
        }
    }
}
