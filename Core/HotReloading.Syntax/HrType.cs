namespace HotReloading.Syntax
{
    public class HrType : BaseHrType
    {
        /// <summary>
        /// Assembly Name
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// Assembly Qualified FullName
        /// </summary>
        public override string AssemblyQualifiedName => $"{Name}, {AssemblyName}";

        /// <summary>
        /// Convert to .net type
        /// </summary>
        /// <param name="hrType"></param>
        public static implicit operator System.Type(HrType hrType)
        {
            return hrType == null ? null : System.Type.GetType(hrType.AssemblyQualifiedName);
        }
    }
}