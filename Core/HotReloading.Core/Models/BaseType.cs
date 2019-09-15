namespace HotReloading.Core
{
    public abstract class BaseType
    {
        /// <summary>
        /// Fullname of the type
        /// </summary>
        public string Name { get; set; }

        public static implicit operator System.Type(BaseType hrType)
        {
            if (hrType is GenericType genericType)
                return genericType;

            return (Type)hrType;
        }
    }
}