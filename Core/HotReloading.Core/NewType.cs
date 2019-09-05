using System;
using System.Collections.Generic;

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
        public ClassType1 ReturnType { get; set; }
        public ClassType1 ParentType { get; set; }
        public AccessModifier AccessModifier { get; set; }
    }

    public class Parameter1
    {
        public string Name { get; set; }
        public ClassType1 Type { get; set; }
    }

    public class ClassType1
    {
        public string Name { get; set; }
        public string AssemblyName { get; set; }

        public string TypeString => $"{Name}, {AssemblyName}";
        public bool IsGeneric { get; set; }

        public static implicit operator Type(ClassType1 classType)
        {
            if (classType.IsGeneric)
            {
                return typeof(object);
            }
            return classType == null ? null : Type.GetType(classType.TypeString);
        }
    }
}
