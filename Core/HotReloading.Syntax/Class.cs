using System.Collections.Generic;

namespace HotReloading.Syntax
{
    public class Class : ICSharpSyntax
    {
        public AccessModifier AccessModifier { get; set; }
        public string Name { get; set; }
        public bool IsStatic { get; set; }
        public bool IsSealed { get; set; }
        public bool IsAbstract { get; set; }
        public List<Field> Fields { get; set; }
        public List<Property> Properties { get; set; }
        public List<Method> Methods { get; set; }

        public Class()
        {
            Fields = new List<Field>();
            Properties = new List<Property>();
            Methods = new List<Method>();
        }
    }
}