using System;
using System.Linq;
using Gtk;

namespace VisualStudio.Mac.Extensions
{
    public static class WidgetExtensions
    {
        public static void RemoveFromContainer(this Widget self, Widget child)
        {
            var container = self as Container;

            if (child != null && child.Parent != null)
            {
                if (container != null && container.HasChild(child))
                {
                    container.Remove(child);
                }
            }
        }

        public static bool HasChild(this Container self, Widget child)
        {
            return self.Children.Contains(child);
        }
    }
}
