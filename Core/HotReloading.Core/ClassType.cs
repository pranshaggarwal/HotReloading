using System;

namespace HotReloading.Core
{
    public class ClassType
    {
        public string Name { get; set; }
        public string AssemblyName { get; set; }

        public string TypeString => $"{Name}, {AssemblyName}";

        public static implicit operator Type(ClassType classType)
        {
            return classType == null ? null : Type.GetType(classType.TypeString);
        }
    }
}