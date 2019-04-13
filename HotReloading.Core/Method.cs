using System.Collections.Generic;
using HotReloading.Core.Statements;

namespace HotReloading.Core
{
    public class Method
    {
        public string Name { get; set; }
        public List<Parameter> Parameters { get; set; }
        public Block Block { get; set; }
        public bool IsStatic { get; set; }
        public bool IsAsync { get; set; }
        public ClassType ReturnType { get; set; }
        public ClassType ParentType { get; set; }
        public AccessModifier AccessModifier { get; set; }
    }
}