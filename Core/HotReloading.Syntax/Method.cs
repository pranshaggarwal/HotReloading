using System.Collections.Generic;
using HotReloading.Syntax.Statements;

namespace HotReloading.Syntax
{
    public class Method : ICSharpSyntax
    {
        public string Name { get; set; }
        public List<Parameter> Parameters { get; set; }
        public Block Body { get; set; }
        public bool IsStatic { get; set; }
        public bool IsSealed { get; set; }
        public bool IsVirtual { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsOverrided { get; set; }
        public bool IsAsync { get; set; }
        public BaseHrType ReturnType { get; set; }
        public HrType ParentType { get; set; }
        public AccessModifier AccessModifier { get; set; }
        public List<Method> InnerMethods { get; set; }

        public Method()
        {
            Parameters = new List<Parameter>();
            InnerMethods = new List<Method>();
        }
    }
}