using System;
namespace VisualStudio.Mac.CoreUI
{
    [Flags]
    public enum FontAttributes
    {
        None = 0,
        Bold = 1 << 0,
        Italic = 1 << 1
    }
}
