using System;

namespace HotReloading.Core
{
    public class ClassType
    {
        public string Name { get; set; }
        public string AssemblyName { get; set; }
        public string Key
        {
            get
            {
                if (IsGeneric)
                    return Name;
                return ((Type)this).FullName;
            }
        }

        public string TypeString => $"{Name}, {AssemblyName}";
        public bool IsGeneric { get; set; }

        public static implicit operator Type(ClassType classType)
        {
            if(classType.IsGeneric)
            {
                return typeof(object);
            }
            return classType == null ? null : Type.GetType(classType.TypeString);
        }
    }
}