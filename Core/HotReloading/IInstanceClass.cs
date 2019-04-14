using System;
using System.Collections.Generic;

namespace HotReloading
{
    public interface IInstanceClass
    {
        Dictionary<string, Delegate> InstanceMethods { get; }
    }
}