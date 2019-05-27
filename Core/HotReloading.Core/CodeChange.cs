using System.Collections.Generic;

namespace HotReloading.Core
{
    public class CodeChange
    {
        public List<Method> Methods { get; set; } = new List<Method>();
        public List<Field> Fields { get; set; } = new List<Field>();
        public List<Property> Properties { get; set; } = new List<Property>();
    }
}