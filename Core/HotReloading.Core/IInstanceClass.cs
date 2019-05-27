using System;
using System.Collections.Generic;

namespace HotReloading.Core
{
    public interface IInstanceClass
    {
        Dictionary<string, Delegate> InstanceMethods { get; }
        Dictionary<string, FieldContainer> InstanceFields { get; }
        Dictionary<string, IPropertyContainer> InstanceProperties { get; }
    }
}