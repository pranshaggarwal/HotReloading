namespace HotReloading.Core
{
    public class Property
    {
        public string Name { get; set; }
        public Method Getter { get; set; }
        public Method Setter { get; set; }
        public ClassType Type { get; set; }
        public ClassType ParentType { get; set; }
        public bool IsStatic { get; set; }
    }
}