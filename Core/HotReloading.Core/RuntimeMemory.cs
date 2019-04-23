using System;
using System.Collections.Generic;

namespace HotReloading.Core
{
    public class RuntimeMemory
    {
        public static Dictionary<Type, List<IMethodContainer>> Methods { get; private set; } = new Dictionary<Type, List<IMethodContainer>>();
    }
}