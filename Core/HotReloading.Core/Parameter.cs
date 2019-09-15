using HotReloading.Core.Statements;

namespace HotReloading.Core
{
    public class Parameter : Statement
    {
        public string Name { get; set; }
        public BaseType Type { get; set; }
    }
}