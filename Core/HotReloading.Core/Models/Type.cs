using System;
using Newtonsoft.Json;

namespace HotReloading.Core
{
    public class Type
    {
        /// <summary>
        /// Fullname of the type
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Assembly Name
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// Is true if type generic parameter type e.g. T
        /// </summary>
        public bool IsGeneric { get; set; }

        public override string ToString()
        {
            return $"{Name}, {AssemblyName}";
        }

        /// <summary>
        /// Convert to .net type
        /// </summary>
        /// <param name="classType"></param>
        public static implicit operator System.Type(Type classType)
        {
            if(classType.IsGeneric)
            {
                return typeof(object);
            }
            return classType == null ? null : System.Type.GetType(classType.ToString());
        }
    }
}