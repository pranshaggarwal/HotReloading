namespace HotReloading.Core
{
    public class GenericType : BaseType
    {
        public override string ToString()
        {
            return Name;
        }

        public static implicit operator System.Type(GenericType hrType)
        {
            return typeof(object);
        }
    }
}