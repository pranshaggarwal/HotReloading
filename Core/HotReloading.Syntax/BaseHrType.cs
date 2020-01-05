namespace HotReloading.Syntax
{
    public abstract class BaseHrType
    {
        /// <summary>
        /// Fullname of the type
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Assembly Qualified FullName
        /// </summary>
        public virtual string AssemblyQualifiedName => Name;

        public static implicit operator System.Type(BaseHrType hrType)
        {
            if (hrType is GenericHrType genericType)
                return genericType;

            return (HrType)hrType;
        }
    }
}