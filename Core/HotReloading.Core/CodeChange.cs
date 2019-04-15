using System.Collections.Generic;

namespace HotReloading.Core
{
    public class CodeChange
    {
        public List<Method> Methods { get; set; } = new List<Method>();
    }
}