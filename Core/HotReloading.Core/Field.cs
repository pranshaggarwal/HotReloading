namespace HotReloading.Core
{
    public class Field
    {
        public string Name { get; set; }
        public ClassType Type { get; set; }
        public AccessModifier AccessModifier { get; set; }
        public ClassType ParentType { get; set; }
        public bool IsStatic { get; set; }
    }
}