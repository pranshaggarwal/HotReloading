using System.Drawing;
using System.Security;
using System.Text;
using Gtk;
using VisualStudio.Mac.CoreUI;
using VisualStudio.Mac.Extensions;
using VisualStudio.Mac.Helpers;

namespace VisualStudio.Mac.Controls
{
    public class CustomLabel : Label
    {
        private readonly string text;
        private string fontFamily;
        private double fontSize = 11;
        private FontAttributes fontAttributes;
        private Color textColor;

        public string FontFamily
        {
            get => fontFamily;
            set
            {
                if (fontFamily != value)
                {
                    fontFamily = value;
                    UpdateText();
                }
            }
        }

        public double FontSize
        {
            get => fontSize;
            set
            {
                if (fontSize != value)
                {
                    fontSize = value;
                    UpdateText();
                }
            }
        }

        public FontAttributes FontAttributes
        {
            get => fontAttributes;
            set
            {
                if (fontAttributes != value)
                {
                    fontAttributes = value;
                    UpdateText();
                }
            }
        }

        public Color TextColor
        {
            get => textColor;
            set
            {
                if(textColor != value)
                {
                    textColor = value;
                    UpdateColor();
                }
            }
        }

        public CustomLabel(string text)
        {
            this.text = text;

            UpdateText();
        }

        private void UpdateText()
        {
            Markup = GenerateMarkupText();
        }

        private void UpdateColor()
        {
            ModifyFg(StateType.Normal, TextColor.ToGdkColor());
        }

        private string GenerateMarkupText()
        {
            var builder = new StringBuilder();

            builder.Append("<span ");

            var fontDescription = FontDescriptionHelper.CreateFontDescription(
                FontSize, FontFamily, FontAttributes);

            builder.AppendFormat(" font=\"{0}\"", fontDescription.ToString());

            builder.Append(">"); // Complete opening span tag

            // Text
            builder.Append(SecurityElement.Escape(text));
            builder.Append("</span>");

            return builder.ToString();
        }
    }
}
