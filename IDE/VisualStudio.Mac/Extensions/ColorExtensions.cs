namespace VisualStudio.Mac.Extensions
{
    public static class ColorExtensions
    {
        public static Gdk.Color ToGdkColor(this System.Drawing.Color color)
        {
            return new Gdk.Color(color.R, color.G, color.B);
        }
    }
}
