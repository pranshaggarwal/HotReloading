using HotReloading.Core.Statements;
using System.Collections.Generic;

namespace HotReloading.Core
{
    public class Method
    {
        public string Name { get; set; }
        public List<Parameter> Parameters { get; set; }
        public Block Block { get; set; }
        public bool IsStatic { get; set; }
        public bool IsAsync { get; set; }
        public BaseHrType ReturnType { get; set; }
        public HrType ParentType { get; set; }
        public AccessModifier AccessModifier { get; set; }
    }
}