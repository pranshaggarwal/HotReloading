namespace HotReloading.Syntax
{
    public class GenericHrType : BaseHrType
    {
        public static implicit operator System.Type(GenericHrType hrType)
        {
            return typeof(object);
        }
    }
}