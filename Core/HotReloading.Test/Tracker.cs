namespace HotReloading.Test
{
    public static class Tracker
    {
        public static object LastValue { get; set; }

        public static void Call(object value)
        {
            LastValue = value;
        }

        public static void Reset()
        {
            LastValue = null;
        }
    }
}
