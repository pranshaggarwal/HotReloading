using System;

namespace HotReloading.BuildTask
{
    public class Method
    {
        public Type ParentType { get; set; }

        public string MethodName { get; set; }

        public Type[] ParameterSignature { get; set; }
    }
}