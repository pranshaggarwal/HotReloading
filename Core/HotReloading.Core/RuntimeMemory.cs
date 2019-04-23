using System;
using System.Collections.Generic;

namespace HotReloading.Core
{
    public class RuntimeMemory
    {
        public static List<IInstanceClass> MemoryInstances = new List<IInstanceClass>();
        public static Dictionary<Type, List<IMethodContainer>> Methods { get; private set; } = new Dictionary<Type, List<IMethodContainer>>();

        public static void Reset()
        {
            MemoryInstances.Clear();
            Methods.Clear();
        }
    }
}