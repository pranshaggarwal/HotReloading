using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExpressions.Microsoft.CSharp;

namespace HotReloading.Core
{
    public class RuntimeMemory
    {
        public static List<IInstanceClass> MemoryInstances = new List<IInstanceClass>();
        public static Dictionary<System.Type, List<IMethodContainer>> Methods { get; private set; } = new Dictionary<System.Type, List<IMethodContainer>>();

        public static CSharpLamdaExpression GetMethod(System.Type @class, string key)
        {
            if (RuntimeMemory.Methods.ContainsKey(@class))
            {
                var method = RuntimeMemory.Methods[@class].SingleOrDefault(x => Helper.GetMethodKey(x.Method) == key);
                if (method != null)
                    return method.GetExpression();
            }

            return null;
        }

        public static void Reset()
        {
            MemoryInstances.Clear();
            Methods.Clear();
        }
    }
}