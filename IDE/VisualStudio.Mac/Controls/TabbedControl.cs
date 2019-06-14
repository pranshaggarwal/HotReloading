using Gtk;

namespace VisualStudio.Mac.Controls
{
    public class TabbedControl : Notebook
    {
        int index = 0;

        public TabbedControl()
        {
            CanFocus = true;
            Scrollable = true;
            ShowTabs = true;
            TabPos = PositionType.Top;
        }

        public void AddPage(Widget widget, string title)
        {
            var tab_label = new Label(title);
            InsertPage(widget, tab_label, index);
            index++;
        }
    }
}
