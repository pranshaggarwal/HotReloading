using System;
namespace StatementConverter.Test
{
    public class ParameterTestClass
    {
        public static void MethodWithParameter(string test)
        {
            Tracker.Call(test);
        }
    }
}
