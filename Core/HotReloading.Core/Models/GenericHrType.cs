namespace HotReloading.Core
{
    public class GenericHrType : BaseHrType
    {
        public static implicit operator System.Type(GenericHrType hrType)
        {
            return typeof(object);
        }
    }
}