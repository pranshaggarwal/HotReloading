using System;
using System.Collections.Generic;

namespace HotReloading.Core
{
    public interface IInstanceClass
    {
        Dictionary<string, Delegate> InstanceMethods { get; }
    }
}