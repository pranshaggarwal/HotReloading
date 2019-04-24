using System;
using Newtonsoft.Json;

namespace HotReloading.Core
{
    public class ClassType
    {
        public string Name { get; set; }
        public string AssemblyName { get; set; }
        [JsonIgnore]
        public string Key
        {
            get
            {
                if (IsGeneric)
                    return Name;

                var type = ((Type)this);

                var key = type.Namespace + "." + type.Name;
                if(type.IsGenericType)
                {
                    key += "<";
                    foreach(var genericArgument in type.GenericTypeArguments)
                    {
                        key += genericArgument.Namespace + "." + genericArgument.Name + ",";
                    }
                    key = key.Remove(key.Length - 1);
                    key += ">";
                }
                return key;
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