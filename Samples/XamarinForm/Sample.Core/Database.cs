using System;
using System.Collections.Generic;

namespace Sample.Core
{
    public class Database
    {
        public static void Log(string message)
        {
            System.Diagnostics.Debug.WriteLine("log2: " + message);
        }
    }

    public class Person
    {
        public string Name { get; set; }
    }
}
