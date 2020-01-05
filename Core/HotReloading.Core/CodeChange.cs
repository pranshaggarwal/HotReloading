using System.Collections.Generic;
using HotReloading.Syntax;

namespace HotReloading.Core
{
    public class CodeChange
    {
        public List<Method> Methods { get; set; } = new List<Method>();
    }
}