using System;

namespace BaseAssembly
{
    public static class Tracker
    {
        public static object LastValue { get; set; }

        public static void Call(object value)
        {
            LastValue = value;
        }
    }
}
