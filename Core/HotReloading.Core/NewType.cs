using System;
using System.Collections.Generic;
using System.Linq;

namespace HotReloading.Core
{
    class NewType
    {
    }

    public class Method1
    {
        public string Name { get; set; }
        public List<Parameter1> Parameters { get; set; }
        //public Block Block { get; set; }
        public bool IsStatic { get; set; }
        public bool IsAsync { get; set; }
        public HrType ReturnType { get; set; }
        public HrType ParentType { get; set; }
        public AccessModifier AccessModifier { get; set; }

        public override string ToString()
        {
            var staticKeyword = IsStatic ? "static" : "";
            var asyncKeyword = IsAsync ? "async" : "";
            var methodStr = $"{AccessModifier} {staticKeyword} {asyncKeyword} {ReturnType} {Name}(";
            var parameter = Parameters.Aggregate("", (lastResult, p) =>
            {
                if(!string.IsNullOrWhiteSpace(lastResult))
                {
                    lastResult += ", ";
                }
                return lastResult + p.ToString();
            });

            methodStr += ")\n{";
            methodStr += "\n}";
            return methodStr;
        }
    }

    public class Parameter1
    {
        public string Name { get; set; }
        public HrType Type { get; set; }
    }

    //public class ClassType1
    //{
    //    public string Name { get; set; }
    //    public string AssemblyName { get; set; }

    //    public string TypeString => $"{Name}, {AssemblyName}";
    //    public bool IsGeneric { get; set; }

    //    public static implicit operator Type(ClassType1 classType)
    //    {
    //        if (classType.IsGeneric)
    //        {
    //            return typeof(object);
    //        }
    //        return classType == null ? null : Type.GetType(classType.TypeString);
    //    }
    //}
}
