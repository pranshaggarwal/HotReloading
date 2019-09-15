using System;
using Newtonsoft.Json;

namespace HotReloading.Core
{
    public class Type : BaseType
    {
        /// <summary>
        /// Assembly Name
        /// </summary>
        public string AssemblyName { get; set; }

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
            return classType == null ? null : System.Type.GetType(classType.ToString());
        }
    }
}