using HotReloading.Core.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HotReloading.Core
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

    public class Class : ICSharpSyntax
    {
        public List<Field> Fields { get; set; }
        public List<Property> Properties { get; set; }
        public List<Method> Methods { get; set; }
    }

    public class Field : ICSharpSyntax
    {
        public AccessModifier AccessModifier { get; set; }
        public BaseHrType Type { get; set; }
    }

    public class Property : ICSharpSyntax
    {

    }
}