using System.Collections.Generic;

namespace HotReloading.Core
{
    public class CodeChangeRequest
    {
        public string CompileError { get; set; }
        public string ParsingError { get; set; }
        public List<Method> UpdateMethods { get; set; } = new List<Method>();
    }
}