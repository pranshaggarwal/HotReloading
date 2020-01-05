using System.Collections.Generic;

namespace HotReloading.Syntax
{
    public class Class : ICSharpSyntax
    {
        public List<Field> Fields { get; set; }
        public List<Property> Properties { get; set; }
        public List<Method> Methods { get; set; }
    }
}